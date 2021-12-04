using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
    public class PlaceUsersController : ControllerBase
    {
        private readonly PlaceUsersRepository _placeUsersRepository;
        private readonly IMapper _mapper;

        public PlaceUsersController(PlaceUsersRepository placeUsersRepository, IMapper mapper)
        {
            _placeUsersRepository = placeUsersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<PlaceUserDTO> GetPlaceUsers()
        {
            return _placeUsersRepository.Get().Select(_mapper.Map<PlaceUserDTO>);
        }

        [HttpPost]
        public ActionResult<PlaceUser> PostPlaceUsers([FromBody] PlaceUser placeUser)
        {
            var newPlaceUser = _placeUsersRepository.Create(placeUser);
            return CreatedAtAction(nameof(GetPlaceUsers), new {id = newPlaceUser.Id}, newPlaceUser);
        }
    }
}