using System.Collections.Generic;
using System.Linq;
using DataAPI.Models.Users;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAPI.Repositories
{
    public class ClientUsersRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public ClientUsersRepository(AppDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<ClientUser> Get()
        {
            return _dbContext.ClientUsers
                .Include(c => c.Ratings)
                .ToList();
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