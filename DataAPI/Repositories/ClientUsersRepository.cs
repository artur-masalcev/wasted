using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Repositories
{
    public class ClientUsersRepository
    {
        private readonly AppContext _context;

        public ClientUsersRepository(AppContext context)
        {
            _context = context;
        }
        
        public IEnumerable<ClientUser> Get()
        {
            return _context.ClientUsers
                .Include(c => c.Ratings)
                .ToList();
        }

        public ClientUser Create(ClientUser clientUser)
        {
            _context.ClientUsers.Add(clientUser);
            _context.SaveChangesAsync();
            return clientUser;
        }
    }
}