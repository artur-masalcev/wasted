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
        private readonly DealsRepository _dealsRepository;
        private readonly IMapper _mapper;

        public DealsController(DealsRepository dealsRepository, IMapper mapper)
        {
            _dealsRepository = dealsRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public DealDTO GetDealById(int id)
        {
            return _mapper.Map<DealDTO>(_dealsRepository.GetDeal(id));
        }
        
        [HttpGet("special/{specialOffersCount:int}")]
        public IEnumerable<DealDTO> GetBestOffers(int specialOffersCount)
        {
            return _dealsRepository.GetBestOffers(specialOffersCount)
                .Select(_mapper.Map<DealDTO>);
        }

        [HttpPost]
        public ActionResult<Deal> PostDeal([FromBody] Deal deal)
        {
            var newDeal = _dealsRepository.Create(deal);
            return CreatedAtAction(nameof(PostDeal), new {id = newDeal.Id}, newDeal); //TODO: investigate action naming
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