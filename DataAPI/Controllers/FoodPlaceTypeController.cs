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
        private readonly FoodPlaceTypeRepository _placeTypeRepository;
        private readonly IMapper _mapper;

        public FoodPlaceTypesController(FoodPlaceTypeRepository placeTypeRepository, IMapper mapper)
        {
            _placeTypeRepository = placeTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<FoodPlaceTypeDTO> GetFoodPlaceTypes()
        {
            return _placeTypeRepository.Get().Select(_mapper.Map<FoodPlaceTypeDTO>);
        }

        [HttpPost]
        public ActionResult<FoodPlaceType> PostFoodPlaceTypes([FromBody] FoodPlaceType foodPlace)
        {
            var newFoodPlaceType = _placeTypeRepository.Create(foodPlace);
            return CreatedAtAction(nameof(GetFoodPlaceTypes), new {id = newFoodPlaceType.Id}, newFoodPlaceType);
        }
    }
}