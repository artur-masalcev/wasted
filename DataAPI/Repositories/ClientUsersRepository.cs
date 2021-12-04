using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class ClientUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public ClientUsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
            _dbContext.SaveChangesAsync();
            return clientUser;
        }
    }
}