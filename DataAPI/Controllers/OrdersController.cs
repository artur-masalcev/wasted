using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAPI.DTO;
using DataAPI.Models;
using DataAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersController(OrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        [HttpGet("clientusers/{clientUserId:int}")]
        public IEnumerable<OrderDTO> GetClientOrders(int clientUserId)
        {
            return _ordersRepository.Get()
                .Select(_mapper.Map<OrderDTO>)
                .Where(o => o.ClientUserId == clientUserId);
        }
        
        [HttpGet("placeusers/{placeUserId:int}")]
        public IEnumerable<OrderDTO> GetPlaceOrders(int placeUserId)
        {
            return _ordersRepository.Get()
                .Select(_mapper.Map<OrderDTO>)
                .Where(o => o.DealFoodPlacePlaceUserId == placeUserId);
        }

        [HttpPost]
        public ActionResult<Order> PostOrder([FromBody] Order order)
        {
            var newOrder = _ordersRepository.Create(order);
            return CreatedAtAction(nameof(GetClientOrders), new {id = newOrder.Id}, newOrder);
        }
        
        [HttpPut]
        public ActionResult<List<Order>> UpdateOrder([FromBody] List<Order> orders)
        {
            _ordersRepository.Update(orders);
            return orders;
        }
    }
}