using AutoMapper;
using DataAPI.Models;

namespace DataAPI.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Store, StoreDTO>();
            CreateMap<Book, BookDTO>();
        }
    }
}