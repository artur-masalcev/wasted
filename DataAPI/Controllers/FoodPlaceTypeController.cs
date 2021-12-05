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
    public class FoodPlaceTypesController : ControllerBase
    {
        private readonly FoodPlaceTypeRepository _placesRepository;
        private readonly IMapper _mapper;

        public FoodPlaceTypesController(FoodPlaceTypeRepository placesRepository, IMapper mapper)
        {
            _placesRepository = placesRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IEnumerable<FoodPlaceTypeDTO> GetFoodPlaceTypes()
        {
            return _placesRepository.Get().Select(_mapper.Map<FoodPlaceTypeDTO>);
        }

        [HttpPost]
        public ActionResult<FoodPlaceType> PostFoodPlaceTypes([FromBody] FoodPlaceType foodPlace)
        {
            var newFoodPlaceType = _placesRepository.Create(foodPlace);
            return CreatedAtAction(nameof(GetFoodPlaceTypes), new {id = newFoodPlaceType.Id}, newFoodPlaceType);
        }
    }
}