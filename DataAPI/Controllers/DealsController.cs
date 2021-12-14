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
    public class DealsController : ControllerBase
    {
        private readonly IDealsRepository _dealsRepository;
        private readonly IMapper _mapper;

        public DealsController(IDealsRepository dealsRepository, IMapper mapper)
        {
            _dealsRepository = dealsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<DealDTO> GetDeals()
        {
            return _dealsRepository.Get().Select(_mapper.Map<DealDTO>);
        }

        [HttpPost]
        public ActionResult<Deal> PostDeal([FromBody] Deal deal)
        {
            var newDeal = _dealsRepository.Create(deal);
            return CreatedAtAction(nameof(GetDeals), new {id = newDeal.Id}, newDeal);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteDeal(int id)
        {
            _dealsRepository.Delete(id);
            return NoContent();
        }

        [HttpPut]
        public ActionResult<Deal> UpdateDeal([FromBody] Deal deal)
        {
            _dealsRepository.Update(deal);
            return deal;
        }
    }
}