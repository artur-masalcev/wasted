using System;
using System.Collections.Generic;
using System.Linq;
using DataAPI;
using DataAPI.Models;
using DataAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DataAPI.Tests
{
    public class RatingsRepositoryIntegrationTest
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "temp_database")
            .Options;

        [Test]
        public void ShouldReturnRating_WhenRatingAdded()
        {
            using var context = new AppDbContext(_dbContextOptions);
            RatingsRepository ratingsRepository = new RatingsRepository(context);
            ratingsRepository.Create(new Rating());
            IEnumerable<Rating> ratings = ratingsRepository.Get();
            Assert.True(ratings.Any());
        }

        [Test]
        public void ShouldReturnNoRatings()
        {
            using var context = new AppDbContext(_dbContextOptions);
            RatingsRepository ratingsRepository = new RatingsRepository(context);
            IEnumerable<Rating> ratings = ratingsRepository.Get();
            Assert.False(ratings.Any());
        }
    }
}