using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Service.IService;

namespace BookStoreAPI.Controller
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        IImageService _image;
        IMapper _map;
        public ImageController(IImageService image, IMapper mapper)
        {
            _image = image;
            _map = mapper;
        }

        [HttpGet("getImage")]
        public async Task<IActionResult> GetImage(Guid bookId)
        {
            var respone = await _image.GetAllImage(bookId);
            if (respone != null)
            {
                return Ok(respone);
            }
            return BadRequest("null");
        }
        [HttpPost("addImage")]
        public async Task<IActionResult> AddImage(ImageDTO imageDTO)
        {
            if (imageDTO != null)
            {
                var image = _map.Map<ImageBook>(imageDTO);
                var result = await _image.CreateImage(image);
                if (result) return Ok("Add Image Success");
            }
            return BadRequest("Add Image Fail");
        }
        [HttpPut("updateImage")]
        public async Task<IActionResult> UpdateImage(ImageDTO imageDTO)
        {
            if (imageDTO != null)
            {
                var image= _map.Map<ImageBook>(imageDTO);
                var result = await _image.UpdateImage(image);
                if (result) return Ok("Update Image Success");
            }
            return BadRequest("Update Image Fail");
        }
    }
}
