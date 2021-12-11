using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models.Users;
using DataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientUsersController : ControllerBase
    {
        private readonly ClientUsersRepository _clientUsersRepository;
        private readonly IMapper _mapper;

        public ClientUsersController(ClientUsersRepository clientUsersRepository, IMapper mapper)
        {
            _clientUsersRepository = clientUsersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ClientUserDTO> GetClientUsers()
        {
            return _clientUsersRepository.Get().Select(_mapper.Map<ClientUserDTO>);
        }

        [HttpGet("{username}/{password}")]
        public ClientUserDTO GetClientUser(string username, string password)
        {
            return _clientUsersRepository
                .Get()
                .Select(_mapper.Map<ClientUserDTO>)
                .FirstOrDefault(user => user.Username == username && user.Password == password);
        }

        [HttpPost]
        public ActionResult<ClientUser> PostClientUser([FromBody] ClientUser clientUser)
        {
            var newClientUser = _clientUsersRepository.Create(clientUser);
            return CreatedAtAction(nameof(GetClientUsers), new {id = newClientUser.Id}, newClientUser);
        }
    }
}