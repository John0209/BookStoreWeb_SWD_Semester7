using AutoMapper;
using BookStoreAPI.Core.DTO;
using BookStoreAPI.Core.Model;

namespace BookStoreAPI.Helper
{
    public class Mapping:Profile
    {
        public Mapping()
        { 
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<BookDetailDTO,Book>().ReverseMap();
            CreateMap<BookDTO, Book>().ReverseMap();
            CreateMap<ImageDTO,ImageBook>().ReverseMap();
            CreateMap<InventoryDTO, Inventory>().ReverseMap();
            CreateMap<ImportationDTO, Importation>().ReverseMap();
            CreateMap<RequestDTO, BookingRequest>().ReverseMap();
            CreateMap<ImportationDetailDTO, ImportationDetail>().ReverseMap();
            CreateMap<OrderDTO,Order>().ReverseMap();
            CreateMap<OrderDetailDTO, OrderDetail>().ReverseMap();
        }

    }
}
