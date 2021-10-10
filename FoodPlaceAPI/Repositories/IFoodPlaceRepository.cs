using FoodPlaceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlaceAPI.Repositories
{
    public interface IFoodPlaceRepository
    {
        Task<IEnumerable<FoodPlace>> Get();
        Task<FoodPlace> Get(int id);
        Task<FoodPlace> Create(FoodPlace foodPlace);
        Task Update(FoodPlace foodPlace);
        Task Delete(int id);
    }
}
