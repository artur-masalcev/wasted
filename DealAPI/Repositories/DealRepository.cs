using DealAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DealAPI.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly DealContext _context;

        public DealRepository(DealContext context)
        {
            _context = context;
        }

        public async Task<Deal> Create(Deal deal)
        {
            _context.deals.Add(deal);
            await _context.SaveChangesAsync();

            return deal;
        }

        public async Task Delete(int id)
        {
            var dealToDelete = await _context.deals.FindAsync(id);
            _context.deals.Remove(dealToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Deal>> Get()
        {
            var myEntity = _context.deals.ToList();
            using FileStream stream = File.Create(@".\Deals.json");
            string json = JsonConvert.SerializeObject(myEntity, Formatting.Indented);
            using var sr = new StreamWriter(stream);
            sr.Write(json);
            return await _context.deals.ToListAsync();
        }

        public async Task<Deal> Get(int id)
        {
            var myEntity = _context.deals.Find(id);
            using FileStream stream = File.Create(@".\Deal.json");
            string json = JsonConvert.SerializeObject(myEntity, Formatting.Indented);
            using var sr = new StreamWriter(stream);
            sr.Write(json);
            return await _context.deals.FindAsync(id);
        }

        public async Task Update(Deal deal)
        {
            _context.Entry(deal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
