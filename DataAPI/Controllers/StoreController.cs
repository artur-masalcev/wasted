using System.Collections.Generic;
using System.Threading.Tasks;
using DataAPI.Models;
using DataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreRepository _bookRepository;

        public StoreController(StoreRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Store>> GetBooks()
        {
            return await _bookRepository.Get();
        }
        

        [HttpPost]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Store book)
        {
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new {id = newBook.StoreId}, newBook);
        }
    }
}