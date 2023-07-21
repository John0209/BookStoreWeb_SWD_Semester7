using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DiplayDTO
{
    public class DisplayInventoryDTO
    {
        public Guid Inventory_Id { get; set; }
        public string Image_URL { get; set; }
        public string Book_Title { get; set; }
        public int Inventory_Quantity { get; set; }
        public string Inventory_Note { get; set; }
        public DateTime Inventory_Date_Into { get; set; }
        public string User_Name { get; set; }
        public bool Is_Inventory_Status { get; set; }
    }
}
