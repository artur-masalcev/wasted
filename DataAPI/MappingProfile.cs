using AutoMapper;
using DataAPI.Models;

namespace DataAPI.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FoodPlace, FoodPlaceDTO>();
            CreateMap<Deal, DealDTO>();
            CreateMap<PlaceUser, PlaceUserDTO>();
            CreateMap<ClientUser, ClientUserDTO>();
        }
    }
}