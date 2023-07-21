using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.Model
{
    public class ImportationDetail
    {
        public Guid Import_Detail_Id { get; set; }
        public Guid Import_Id { get; set; }
        public Guid Book_Id { get; set; }
        public int Import_Detail_Quantity { get; set; }
        public float Import_Detail_Price { get; set; }
        public float Import_Detail_Amount { get; set; }
        public Book Book { get; set; }
        public Importation Importation { get; set; }
    }
}
