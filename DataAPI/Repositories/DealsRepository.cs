using System;
using System.Collections.Generic;
using System.Linq;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataAPI.Repositories
{

    public class DealsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public DealsRepository(AppDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<Deal> Get()
        {
            return _dbContext.Deals
                .Include(d => d.FoodPlace)
                .ToList();
        }

        public Deal Create(Deal deal)
        {
            _dbContext.Deals.Add(deal);
            _logger.Information(
                "A new deal \"{DealTitle}\" (Deal id: {DealId}) has been added.\n",
                deal.Title, deal.Id);
            _dbContext.SaveChangesAsync();
            return deal;
        }

        public void Delete(int dealId)
        {
            var dealToDelete = _dbContext.Deals.Find(dealId);
            _dbContext.Deals.Remove(dealToDelete);
            _logger.Information(
                "Deal \"{DealTitle}\" (Deal id: {DealId}) has been deleted.\n",
                dealToDelete.Title, dealToDelete.Id);
            _dbContext.SaveChangesAsync();
        }

        public void Update(Deal updatedDeal)
        {
            var selectedDeal = _dbContext.Deals.Find(updatedDeal.Id);

            selectedDeal.Description = updatedDeal.Description;
            selectedDeal.Due = updatedDeal.Due;
            selectedDeal.Quantity = updatedDeal.Quantity;
            selectedDeal.Title = updatedDeal.Title;
            selectedDeal.CurrentCost = updatedDeal.CurrentCost;
            selectedDeal.PreviousCost = updatedDeal.PreviousCost;
            selectedDeal.ImageURL = updatedDeal.ImageURL;

            _dbContext.SaveChangesAsync();
        }
    }
}