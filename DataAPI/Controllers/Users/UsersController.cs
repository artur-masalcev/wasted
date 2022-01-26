using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models.Users;
using DataAPI.Repositories;
using DataAPI.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController
    {
        private readonly IEnumerable<IUsersController<AbstractUser>> _userControllers;

        public UsersController(IEnumerable<IUsersController<AbstractUser>> userControllers)
        {
            _userControllers = userControllers;
        }

        [HttpGet("{username}/{password}")]
        public AbstractUser GetUser(string username, string password)
        {
            return _userControllers.Select(controller => controller.GetByUsernameAndPassword(username, password))
                .FirstOrDefault(user => user != null);
        }

    }
}