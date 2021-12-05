using AutoMapper;
using DataAPI.Models;

namespace DataAPI.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FoodPlace, FoodPlaceDTO>().ReverseMap();
            CreateMap<Deal, DealDTO>();
            CreateMap<PlaceUser, PlaceUserDTO>();
            CreateMap<ClientUser, ClientUserDTO>();
            CreateMap<Rating, RatingDTO>();
            CreateMap<FoodPlaceType, FoodPlaceTypeDTO>();
        }
    }
}