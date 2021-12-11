using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models;
using DataAPI.Models.Users;
using DataAPI.Repositories;
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

        [HttpGet("{username}/{password}")]
        public PlaceUserDTO GetPlaceUser(string username, string password)
        {
            return _placeUsersRepository
                .Get()
                .Select(_mapper.Map<PlaceUserDTO>)
                .FirstOrDefault(user => user.Username == username && user.Password == password);
        }

        [HttpPost]
        public ActionResult<PlaceUser> PostPlaceUser([FromBody] PlaceUser placeUser)
        {
            var newPlaceUser = _placeUsersRepository.Create(placeUser);
            return CreatedAtAction(nameof(GetPlaceUsers), new {id = newPlaceUser.Id}, newPlaceUser);
        }
    }
}