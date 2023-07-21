using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DiplayDTO
{
    public class DisplayOrderDetailDTO
    {
        public Guid Order_Id { get; set; }
        public string Image_URL { get; set; }
        public string Book_Title { get; set; }
        public int Order_Detail_Quantity { get; set; }
        public float Order_Detail_Amount { get; set; }
        public float Order_Detail_Price { get; set; }
    }
}
