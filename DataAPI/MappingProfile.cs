using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models;
using DataAPI.Models.Users;

namespace DataAPI
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