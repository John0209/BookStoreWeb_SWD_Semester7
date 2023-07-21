using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Service.IService;

namespace BookStoreAPI.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _user;
        IMapper _mapper;
        public UserController(IUserService user,IMapper mapper) 
        {
            _user = user;
            _mapper = mapper;
        }    
       
        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
            var respone = await _user.GetAllUser();
            if (respone != null)
            {
                var user = _mapper.Map<IEnumerable<UserDTO>>(respone);
                return Ok(user);
            }
            
            return BadRequest("null");
        }
        [HttpGet("getUserById")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var respone = await _user.GetUserById(userId);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest(userId + " don't exists");
        }
        [HttpGet("getUserByName")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var respone = await _user.GetUserByName(userName);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest(userName+" don't exists");
        }
        [HttpGet("recoverPassword")]
        public async Task<IActionResult> RecoverPass(string email)
        {
            var respone = await _user.RecoverPassword(email);
            if (respone)
            {
                return Ok("recover password success");
            }
            return BadRequest("recover password fail");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (login != null)
            {
                var respone = await _user.CheckLogin(login);
                if (respone != null)
                {
                    return Ok(respone);
                }
            }
            return BadRequest("Accound or Pass Wrong!");
        }
        [HttpPost("createUserMoble")]
        public async Task<IActionResult> CreateUserMobel(CreateUserDTO userDTO)
        {
            if(userDTO != null)
            {
                var user= _mapper.Map<User>(userDTO);
                var result= await _user.CreateUserMoble(user);
                if(result) return Ok("Create User Success");
            }
            return BadRequest("Create User Fail");
        }
        [HttpPost("createUserFE")]
        public async Task<IActionResult> CreateUserFE(UserDTO userDTO)
        {
            if (userDTO != null)
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _user.CreateUserFE(user);
                if (result) return Ok("Create User Success");
            }
            return BadRequest("Create User Fail");
        }
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(UserDTO userDTO)
        {
            if (userDTO != null)
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _user.UpdateUser(user);
                if (result) return Ok("Update User Success");
            }
            return BadRequest("Update User Fail");
        }
        [HttpPatch("updateRole")]
        public async Task<IActionResult> DeleteUser(Guid userId, int roleID)
        {
            var result = await _user.UpdateRole(userId, roleID);
            if (result) return Ok("Update Role Success");
            return BadRequest("Update Role Fail");
        }
        [HttpPatch("deleteUser")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
                var result = await _user.DeleteUser(userId);
                if (result) return Ok("Delete User Success");
                return BadRequest("Delete User Fail");
        }
       
        [HttpPatch("restoreUser")]
        public async Task<IActionResult> RestoreUser(Guid userId)
        {
            var result = await _user.RestoreUser(userId);
            if (result) return Ok("Restore User Success");
            return BadRequest("Restore User Fail");
        }
        [HttpDelete("removeUser")]
        public async Task<IActionResult> RemoveUser(Guid userId)
        {
            var result = await _user.RemoveUser(userId);
            if (result) return Ok("Remove User Success");
            return BadRequest("Remove User Fail");
        }
    }
}
