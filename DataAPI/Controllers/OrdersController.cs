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

        [HttpGet("clientusers/{clientUserId:int}/{status}")]
        public IEnumerable<OrderDTO> GetClientOrdersByIdAndStatus(int clientUserId, string status)
        {
            return _ordersRepository.GetClientOrdersByIdAndStatus(clientUserId, status)
                .Select(_mapper.Map<OrderDTO>);
        }
        
        [HttpGet("clientusers/{clientUserId:int}/not/{status}")]
        public IEnumerable<OrderDTO> GetClientOrdersByIdAndNotStatus(int clientUserId, string status)
        {
            return _ordersRepository.GetClientOrdersByIdAndNotStatus(clientUserId, status)
                .Select(_mapper.Map<OrderDTO>);
        }

        [HttpGet("placeusers/{placeUserId:int}")]
        public IEnumerable<OrderDTO> GetPlaceOrders(int placeUserId)
        {
            return _ordersRepository.GetPlaceOrdersById(placeUserId)
                .Select(_mapper.Map<OrderDTO>);
        }

        [HttpPost]
        public ActionResult<Order> PostOrder([FromBody] Order order)
        {
            var newOrder = _ordersRepository.Create(order);
            return CreatedAtAction(nameof(PostOrder), new {id = newOrder.Id}, newOrder);
        }

        [HttpPut]
        public ActionResult<List<Order>> UpdateOrders([FromBody] List<Order> orders)
        {
            _ordersRepository.UpdateOrders(orders);
            return orders;
        }
    }
}