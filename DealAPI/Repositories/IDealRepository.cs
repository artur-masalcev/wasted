using DealAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealAPI.Repositories
{
    public interface IDealRepository
    {
        Task<IEnumerable<Deal>> Get();
        Task<Deal> Get(int id);
        Task<Deal> Create(Deal foodPlace);
        Task Update(Deal foodPlace);
        Task Delete(int id);
    }
}
