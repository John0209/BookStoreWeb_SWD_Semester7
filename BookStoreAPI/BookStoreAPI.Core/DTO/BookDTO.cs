using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DTO
{
    public class BookDTO
    {
        public Guid Book_Id { get; set; }
        public Guid Request_Id { get; set; }
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
        public string Image_URL { get; set; }
        public string Book_Title { get; set; }
        public string Book_Author { get; set; }
        public string Book_Description { get; set; }
        public float Book_Price { get; set; }
        public int Book_Quantity { get; set; }
        public int Book_Year_Public { get; set; }
        public int Book_ISBN { get; set; }
        public bool Is_Book_Status { get; set; }
    }
}
