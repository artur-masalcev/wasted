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
    public class RatingsController : ControllerBase
    {
        private readonly RatingsRepository _ratingsRepository;
        private readonly IMapper _mapper;

        public RatingsController(RatingsRepository ratingsRepository, IMapper mapper)
        {
            _ratingsRepository = ratingsRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IEnumerable<Rating> GetRatings()
        {
            return _ratingsRepository.Get();
        }
        
        [HttpPost]
        public ActionResult<Rating> PostRating([FromBody] Rating rating)
        {
            var newRating = _ratingsRepository.Create(rating);
            return CreatedAtAction(nameof(GetRatings), new {id = newRating.Id}, newRating);
        }

        [HttpPut]
        public ActionResult<Rating> UpdateRating([FromBody] Rating rating)
        {
            _ratingsRepository.Update(rating);
            return rating;
        }
    }
}