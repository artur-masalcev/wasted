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
        private readonly FoodPlaceTypesRepository _placeTypesRepository;
        private readonly IMapper _mapper;

        public FoodPlaceTypesController(FoodPlaceTypesRepository placeTypesRepository, IMapper mapper)
        {
            _placeTypesRepository = placeTypesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<FoodPlaceTypeDTO> GetFoodPlaceTypes()
        {
            return _placeTypesRepository.Get().Select(_mapper.Map<FoodPlaceTypeDTO>);
        }

        [HttpPost]
        public ActionResult<FoodPlaceType> PostFoodPlaceTypes([FromBody] FoodPlaceType foodPlace)
        {
            var newFoodPlaceType = _placeTypesRepository.Create(foodPlace);
            return CreatedAtAction(nameof(GetFoodPlaceTypes), new {id = newFoodPlaceType.Id}, newFoodPlaceType);
        }
    }
}