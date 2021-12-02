using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models;
using DataAPI.Repositories;
using DataAPI.Utils;
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
        
        [HttpGet]
        public IEnumerable<DealDTO> GetDeals()
        {
            return _dealsRepository.Get().Select(_mapper.Map<DealDTO>);
        }

        [HttpPost]
        public ActionResult<Deal> PostDeals([FromBody] Deal book)
        {
            var newDeal = _dealsRepository.Create(book);
            return CreatedAtAction(nameof(GetDeals), new {id = newDeal.Id}, newDeal);
        }
    }
}