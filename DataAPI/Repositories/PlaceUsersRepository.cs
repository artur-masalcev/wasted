using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;

namespace DataAPI.Repositories
{
    public class PlaceUsersRepository
    {
        private readonly AppContext _context;

        public PlaceUsersRepository(AppContext context)
        {
            _context = context;
        }
        
        public IEnumerable<PlaceUser> Get()
        {
            return _context.PlaceUsers
                //.Include(p => p.Deals)
                .ToList();
        }

        public PlaceUser Create(PlaceUser placeUser)
        {
            _context.PlaceUsers.Add(placeUser);
            _context.SaveChangesAsync();
            return placeUser;
        }
    }
}