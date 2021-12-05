using System.Collections.Generic;
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
        public RatingsController(RatingsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
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