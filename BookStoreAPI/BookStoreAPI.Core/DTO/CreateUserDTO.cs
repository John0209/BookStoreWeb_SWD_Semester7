using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DTO
{
    public class CreateUserDTO
    {
        public string User_Account { get; set; }
        public string User_Password { get; set; }
        public string User_Email{get; set;}
    }
}
