using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models;
using DataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<FoodPlaceDTO> GetFoodPlaces()
        {
            return _placesRepository.Get().Select(_mapper.Map<FoodPlaceDTO>);
        }

        [HttpPost]
        public ActionResult<FoodPlace> PostFoodPlace([FromBody] FoodPlace foodPlace)
        {
            var newFoodPlace = _placesRepository.Create(foodPlace);
            return CreatedAtAction(nameof(GetFoodPlaces), new {id = newFoodPlace.Id}, newFoodPlace);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteFoodPlace(int id)
        {
            _placesRepository.Delete(id);
            return NoContent();
        }
    }
}