using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models;
using DataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xamarin.Essentials;

namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodPlacesController : ControllerBase
    {
        private readonly PlacesRepository _placesRepository;
        private readonly IMapper _mapper;

        public FoodPlacesController(PlacesRepository placesRepository, IMapper mapper)
        {
            _placesRepository = placesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public FoodPlaceDTO GetFoodPlaceById(int id)
        {
            return _mapper.Map<FoodPlaceDTO>(_placesRepository.GetFoodPlaceById(id));
        }

        [HttpGet("{latitude:double}/{longitude:double}/{nearbyPlacesCount:int}/{maxRadiusInKilometers:int}")]
        public IEnumerable<FoodPlaceDTO> GetClosestFoodPlaces(double latitude, double longitude, int nearbyPlacesCount,
            int maxRadiusInKilometers)
        {
            Location userLocation = new Location(latitude, longitude);
            return _placesRepository.GetClosestFoodPlaces(userLocation, nearbyPlacesCount, maxRadiusInKilometers)
                .Select(_mapper.Map<FoodPlaceDTO>);
        }
        
        [HttpGet("{popularPlacesCount:int}")]
        public IEnumerable<FoodPlaceDTO> GetTopRatedFoodPlaces(int popularPlacesCount)
        {
            return _placesRepository.GetTopRatedFoodPlaces(popularPlacesCount)
                .Select(_mapper.Map<FoodPlaceDTO>);
        }

        [HttpPost]
        public ActionResult<FoodPlace> PostFoodPlace([FromBody] FoodPlace foodPlace)
        {
            var newFoodPlace = _placesRepository.Create(foodPlace);
            return CreatedAtAction(nameof(PostFoodPlace), new {id = newFoodPlace.Id}, newFoodPlace);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteFoodPlace(int id)
        {
            _placesRepository.Delete(id);
            return NoContent();
        }

        [HttpPut]
        public ActionResult<FoodPlace> UpdateFoodPlace([FromBody] FoodPlace foodPlace)
        {
            _placesRepository.Update(foodPlace);
            return foodPlace;
        }
    }
}