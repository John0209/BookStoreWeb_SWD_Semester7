using BookStoreAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.IService
{
    public interface IImageService
    {
        Task<bool> CreateImage(ImageBook image);
        Task<IEnumerable<ImageBook>> GetAllImage(Guid bookId);
        Task<Book> GetImageById(string imageId);
        Task<bool> UpdateImage(ImageBook image);
        Task<bool> DeleteImage(string imageId);
    }
}
