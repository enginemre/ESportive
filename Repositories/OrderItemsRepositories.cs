using SportiveOrder.Context;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class OrderItemsRepositories : GenericRepository<OrderItems>, IOrderItemsRepository
    {
       
        public List<OrderItems> GetOrderProducts(string orderId)
        {
            using var context = new SportiveOrderContext();
           return context.OrderItems.Where(item => item.OrderId == orderId).ToList();
        }
        public void AddOrderItems(OrderItems orderItems)
        {
            using var context = new SportiveOrderContext();
            context.Attach(orderItems);
            context.OrderItems.Add(orderItems);
            context.SaveChanges();

        }
    }
}
