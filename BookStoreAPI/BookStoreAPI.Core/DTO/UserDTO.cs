using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DTO
{
    public class UserDTO
    {
        public Guid User_Id { get; set; }
        public string User_Account { get; set; }
        public string User_Password { get; set; }
        public string User_Email { get; set; }
        public int Role_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Address { get; set; }
        public string User_Phone { get; set; }
        public string Is_User_Gender { get; set; }
        public bool Is_User_Status { get; set; }
    }
}
