using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models.Users;
using DataAPI.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAPI.Repositories
{
    public class ClientUsersRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ClientUsersRepository(AppDbContext dbContext, ILogger logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<ClientUser> Get()
        {
            return _dbContext.ClientUsers
                .Include(c => c.Ratings);
        }

        public ClientUser Create(ClientUser clientUser)
        {
            _dbContext.ClientUsers.Add(clientUser);
            _logger.Information(
                "New client user \"{Username}\" (User id: {UserId}) registered.\n",
                clientUser.Username, clientUser.Id);
            _dbContext.SaveChangesAsync();
            return clientUser;
        }
    }
}