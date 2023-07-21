using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Core.DTO
{
    public class ImageDTO
    {
       public int Image_Id { get; set; }
        public Guid Book_Id { get; set; }
        //public string Image_Name { get; set; }
        public string Image_URL { get; set; }
    }
}
