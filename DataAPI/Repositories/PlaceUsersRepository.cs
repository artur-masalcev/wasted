using System.Collections.Generic;
using System.Linq;
using DataAPI.Models.Users;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAPI.Repositories
{
    public class PlaceUsersRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public PlaceUsersRepository(AppDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<PlaceUser> Get()
        {
            return _dbContext.PlaceUsers
                .Include(p => p.FoodPlaces)
                .ThenInclude(f => f.Deals)
                .ToList();
        }

        public PlaceUser Create(PlaceUser placeUser)
        {
            _dbContext.PlaceUsers.Add(placeUser);
            _logger.Information(
                "New place user \"{Username}\" (User id: {UserId}) registered.\n",
                placeUser.Username, placeUser.Id);
            _dbContext.SaveChangesAsync();
            return placeUser;
        }
    }
}