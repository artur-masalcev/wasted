using FoodPlaceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlaceAPI.Repositories
{
    public class FoodPlaceRepository : IFoodPlaceRepository
    {
        private readonly FoodPlaceContext _context;

        public FoodPlaceRepository(FoodPlaceContext context)
        {
            _context = context;
        }

        public async Task<FoodPlace> Create(FoodPlace foodPlace)
        {
            _context.foodPlaces.Add(foodPlace);
            await _context.SaveChangesAsync();

            return foodPlace;
        }

        public async Task Delete(int id)
        {
            var foodPlaceToDelete = await _context.foodPlaces.FindAsync(id);
            _context.foodPlaces.Remove(foodPlaceToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FoodPlace>> Get()
        {
            var myEntity = _context.foodPlaces.ToList();
            using FileStream stream = File.Create(@".\FoodPlaces.json");
            string json = JsonConvert.SerializeObject(myEntity, Formatting.Indented);
            using var sr = new StreamWriter(stream);
            sr.Write(json);
            return await _context.foodPlaces.ToListAsync();
        }

        public async Task<FoodPlace> Get(int id)
        {
            var myEntity = _context.foodPlaces.Find(id);
            using FileStream stream = File.Create(@".\FoodPlace.json");
            string json = JsonConvert.SerializeObject(myEntity, Formatting.Indented);
            using var sr = new StreamWriter(stream);
            sr.Write(json);
            return await _context.foodPlaces.FindAsync(id);
        }

        public async Task Update(FoodPlace foodPlace)
        {
            _context.Entry(foodPlace).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
