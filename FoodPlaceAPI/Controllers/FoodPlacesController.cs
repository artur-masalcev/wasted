using FoodPlaceAPI.Models;
using FoodPlaceAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodPlacesController : ControllerBase
    {
        private readonly IFoodPlaceRepository _foodPlaceRepository;

        public FoodPlacesController(IFoodPlaceRepository foodPlaceRepository)
        {
            _foodPlaceRepository = foodPlaceRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FoodPlace>> GetFoodPlaces()
        {
            return await _foodPlaceRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodPlace>> GetFoodPlaces(int id)
        {
            return await _foodPlaceRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<FoodPlace>> PostFoodPlaces([FromBody] FoodPlace foodPlace)
        {
            var newFoodPlace = await _foodPlaceRepository.Create(foodPlace);
            return CreatedAtAction(nameof(GetFoodPlaces), new { id = newFoodPlace.ID }, newFoodPlace);
        }

        [HttpPut]
        public async Task<ActionResult> PutFoodPlaces(int id, [FromBody] FoodPlace foodPlace)
        {
            if (id != foodPlace.ID)
            {
                return BadRequest();
            }

            await _foodPlaceRepository.Update(foodPlace);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var foodPlaceToDelete = await _foodPlaceRepository.Get(id);
            if (foodPlaceToDelete == null)
                return NotFound();

            await _foodPlaceRepository.Delete(foodPlaceToDelete.ID);
            return NoContent();
        }
    }
}
