using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ImageService : IImageService
    {
        IUnitOfWorkRepository _unit;
        public ImageService(IUnitOfWorkRepository unit)
        {
            _unit = unit;
        }
        public async Task<bool> CreateImage(ImageBook image)
        {
            if (image != null)
            {
                await _unit.Images.Add(image);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public Task<bool> DeleteImage(string imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ImageBook>> GetAllImage(Guid bookId)
        {
            var image = await _unit.Images.GetAll();
            var imageList= from i in image where i.Book_Id == bookId select i;
            if(imageList.Count()>0) return imageList;
            return null;
        }

        public Task<Book> GetImageById(string imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateImage(ImageBook image)
        {
            var m_update = await _unit.Images.GetById(image.Image_Id);
            if(m_update != null)
            {
                m_update.Image_URL = image.Image_URL;
                m_update.Book_Id = image.Book_Id;
                _unit.Images.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}
