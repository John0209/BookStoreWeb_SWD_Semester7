using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DTO
{
    public class ImportationDetailDTO
    {
        public Guid Import_Detail_Id { get; set; }
        public Guid Request_Id { get; set; }
        public Guid Import_Id { get; set; }
        public Guid Book_Id { get; set; }
        public int Import_Detail_Quantity { get; set; }
        public float Import_Detail_Price { get; set; }
        public float Import_Detail_Amount { get; set; }
    }
}
