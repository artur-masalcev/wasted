using DealAPI.Models;
using DealAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealsController : ControllerBase
    {
        private readonly IDealRepository _dealRepository;

        public DealsController(IDealRepository dealRepository)
        {
            _dealRepository = dealRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Deal>> GetDeals()
        {
            return await _dealRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Deal>> GetDeals(int id)
        {
            return await _dealRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Deal>> PostDeals([FromBody] Deal deal)
        {
            var newDeal = await _dealRepository.Create(deal);
            return CreatedAtAction(nameof(GetDeals), new { id = newDeal.ID }, newDeal);
        }

        [HttpPut]
        public async Task<ActionResult> PutDeals(int id, [FromBody] Deal deal)
        {
            if (id != deal.ID)
            {
                return BadRequest();
            }

            await _dealRepository.Update(deal);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dealToDelete = await _dealRepository.Get(id);
            if (dealToDelete == null)
                return NotFound();

            await _dealRepository.Delete(dealToDelete.ID);
            return NoContent();
        }
    }
}
