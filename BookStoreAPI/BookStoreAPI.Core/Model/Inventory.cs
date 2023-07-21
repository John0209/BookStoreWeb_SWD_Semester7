using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Model
{
    public class Inventory
    {
        public Guid Inventory_Id { get; set; }
        public Guid User_Id { get; set; }
        public Guid Book_Id { get; set; }
        public int Inventory_Quantity { get; set; }
        public string Inventory_Note { get; set; }
        public DateTime Inventory_Date_Into { get; set; }
        public bool Is_Inventory_Status { get; set; }
        public User User { get; set; }
        public Book Books{ get; set; }
    }
}
