using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Service
{
    public class UserService : IUserService
    {
        IUnitOfWorkRepository _unit;
        private User m_user;
        public UserService(IUnitOfWorkRepository unit)
        {
            _unit=unit;
            m_user=new User();
        }

        public async Task<User> CheckLogin(LoginDTO login)
        {
            var user = await _unit.User.GetAll();
            login.User_Password=HashingPassword(login.User_Password);
            if (user != null)
            {
                var result = user.SingleOrDefault(u => u.User_Account == login.User_Account && u.User_Password == login.User_Password);
                if (result != null) return result;
            }
            return null;
        }
        
        public async Task<bool> CreateUserMoble(User user)
        {
            if (user != null)
            {
                //var m_list = await GetAllUser();
                user.User_Id = Guid.NewGuid();
                user.Role_Id = 3;
                user.User_Password= HashingPassword(user.User_Password);
                user.Is_User_Status= true;
                user.User_Phone = "No Data";
                user.User_Address = "No Data";
                user.User_Name= "No Data";
                user.Is_User_Gender = "No Data";
                await _unit.User.Add(user);
                var result=_unit.Save();
                if(result >0)return true;
            }
            return false;
        }

        private string HashingPassword(string input)
        {
            // Tạo đối tượng MD5
            using (MD5 md5 = MD5.Create())
            {
                // Chuyển đổi input thành mảng byte
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Băm dữ liệu
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi kết quả hash thành chuỗi hexa
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public async Task<bool> CreateUserFE(User user)
        {
            if (user != null)
            {
                //var m_list = await GetAllUser();
                user.User_Id = Guid.NewGuid();
                user.User_Password = HashingPassword(user.User_Password);
                user.Role_Id = 3;
                user.Is_User_Status = true;
                await _unit.User.Add(user);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<bool> DeleteUser(Guid userId)
        {
            var m_update = _unit.User.SingleOrDefault(m_user, u => u.User_Id == userId);
            if (m_update != null)
            {
                m_update.Is_User_Status = false;
                _unit.User.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            var result= await _unit.User.GetAll();
            if(result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<User> GetUserById(Guid userId)
        {
           var result=await _unit.User.GetById(userId);
            if (result != null) return result;
            return null;
        }

        public async Task<IEnumerable<User>> GetUserByName(string name)
        {
            var users = await GetAllUser();
            var result = from b in users where (b.User_Name.ToLower().Trim().Contains(name.ToLower().Trim())&& b.Is_User_Status==true) select b;
            if (result.Count() > 0)
            {
                return result;
            }
            return null;
        }
        public async Task<bool> UpdateUser(User user)
        {
            var m_update =await _unit.User.GetById(user.User_Id);
            if (m_update != null)
            {
                m_update.User_Name = user.User_Name;
                m_update.Role_Id= user.Role_Id;
                m_update.User_Account=user.User_Account;
                m_update.User_Password = HashingPassword(user.User_Password);
                m_update.User_Email= user.User_Email;
                m_update.User_Address= user.User_Address;
                m_update.User_Phone= user.User_Phone;
                m_update.Is_User_Gender= user.Is_User_Gender;
                m_update.Is_User_Status= user.Is_User_Status;
                _unit.User.Update(m_update);
                var result= _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RestoreUser(Guid userId)
        {
            var m_update = _unit.User.SingleOrDefault(m_user, u => u.User_Id == userId);
            if (m_update != null)
            {
                m_update.Is_User_Status = true;
                _unit.User.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RemoveUser(Guid userId)
        {
           var user= await _unit.User.GetById(userId);
            if (user != null)
            {
                _unit.User.Delete(user);
                var result = _unit.Save();
                if(result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RecoverPassword(string email)
        {
            var m_user = await CheckEmail(email);
            if (m_user != null)
            {
            // random password với độ dài 8 ký tự
            int lengthOfRandomString = 8;
            string randomPass = GenerateRandomString(lengthOfRandomString);
            // set up send meail
            string sendto = m_user.User_Email;
            string subject = "Recover Password of Account " + m_user.User_Account;
            string content = "Your New Password is "+randomPass;
            //set new password
             m_user.User_Password = randomPass;
            //update password
            await UpdateUser(m_user);
            string _gmail = "nguyentuanvu020901@gmail.com";
            string _pass = "fhnwtwqisekdqzcr";
            try
            {
                
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                //set property for email you want to send
                mail.From = new MailAddress(_gmail);
                mail.To.Add(sendto);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = content;
                mail.Priority = MailPriority.High;
                //set smtp port
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                //set gmail pass sender
                smtp.Credentials = new NetworkCredential(_gmail, _pass);
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            }
            else
                return false;
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        private async Task<User> CheckEmail(string email)
        {
            var listUser = await _unit.User.GetAll();
            var check = (from u in listUser where u.User_Email == email select u).FirstOrDefault();
            if (check != null)
            {
                return check;
            }
            return null;
        }

        public async Task<bool> UpdateRole(Guid userId, int roleId)
        {
            var user = await _unit.User.GetById(userId);
            if (user != null)
            {
                user.Role_Id = roleId;
                _unit.User.Update(user);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}
