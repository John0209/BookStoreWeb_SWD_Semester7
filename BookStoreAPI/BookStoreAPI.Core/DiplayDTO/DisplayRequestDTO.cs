using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DiplayDTO
{
    public class DisplayRequestDTO
    {
        public string Request_Image_Url { get; set; }
        public string Request_Book_Name { get; set; }
        public int Request_Quantity { get; set; }
        public float Request_Price { get; set; }
        public string Request_Note { get; set; }
        public bool Is_RequestBook_Status { get; set; }
    }
}
