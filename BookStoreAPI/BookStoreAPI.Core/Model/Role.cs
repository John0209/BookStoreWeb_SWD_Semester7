using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Model
{
   public class Role
    {
       public int Role_Id { get; set; }
       public string Role_Name { get; set; }
       public ICollection<User> Users { get; set; }
    }
}
