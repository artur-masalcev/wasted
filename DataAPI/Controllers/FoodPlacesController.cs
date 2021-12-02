using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<FoodPlaceDTO> GetBooks()
        {
            return _placesRepository.Get().Select(_mapper.Map<FoodPlaceDTO>);
        }

        [HttpPost]
        public ActionResult<FoodPlace> PostBooks([FromBody] FoodPlace book)
        {
            var newBook = _placesRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new {id = newBook.Id}, newBook);
        }

        // public override string GetPathToData()
        // {
        //     return DummyDataProvider.FoodPlacesJSONPath;
        // }
    }
}
