using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Model
{
    public class Order
    {
        public Guid Order_Id { get; set; }
        public Guid User_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public string Order_Code { get; set; }
        public int Order_Quantity { get; set; }
        public float Order_Amount { get; set; }
        public string Order_Customer_Name { get; set; }
        public string Order_Customer_Address { get; set; }
        public string Order_Customer_Phone { get; set; }
        public int Is_Order_Status { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
