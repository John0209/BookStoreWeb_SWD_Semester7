using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Model
{
    public class BookingRequest
    {
        public Guid Request_Id { get; set; }
        public Guid Book_Id { get; set; }
        public int Category_Id { get; set; }
        public string Request_Image_Url { get; set; }
        public string Request_Book_Name { get; set; }
        public int Request_Quantity { get; set; }
        public float Request_Price { get; set; }
        public float Request_Amount { get; set; }
        public DateTime Request_Date { get; set; }
        public DateTime Request_Date_Done { get; set; }
        public string Request_Note { get; set; }
        public bool Is_RequestBook_Status { get; set; }
        public int Is_Request_Status { get; set; }
        public Book Book { get; set; }
    }
}
