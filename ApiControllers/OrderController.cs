using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportiveOrder.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IProductRepository _productRepository;
        public OrderController(IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository, IProductRepository productRepository)
        {
            _orderItemsRepository = orderItemsRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public List<Order> Get([FromQuery(Name = "UserId")] string userId)
        {
            List<Order> orders = _orderRepository.GetUserOrders(userId);
            foreach (var item in orders)
            {
                item.OrderItems = _orderItemsRepository.GetOrderProducts(item.OrderId);
                foreach (var orderItems in item.OrderItems)
                {
                    orderItems.Product = _productRepository.GetEntity(orderItems.ProductId);
                }
            }
            return orders;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public List<Order> Get([FromQuery(Name = "UserId")] string userId, string id)
        {
            List<Order> orders = _orderRepository.GetUserOrders(userId);
            foreach (var item in orders)
            {
                item.OrderItems = _orderItemsRepository.GetOrderProducts(id);
                foreach (var orderItems in item.OrderItems)
                {
                    orderItems.Product = _productRepository.GetEntity(orderItems.ProductId);
                }
            }
            return orders;
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] Order order)
        {
            
        }
        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
