using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Model
{
    public class OrderDetail
    {
        public Guid Order_Detail_Id { get; set; }
        public Guid Order_Id { get; set; }
        public Guid Book_Id { get; set; }
        public int Order_Detail_Quantity { get; set; }
        public float Order_Detail_Amount { get; set; }
        public float Order_Detail_Price { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
