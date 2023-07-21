using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DiplayDTO
{
   public class FileExcel
    {
        public string NameBook { get; set; }
        public DateTime Date {  get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
        public float amount { get; set; }
    }
}
