using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Interfaces
{
    public interface IOrderRepository : IGenericRepositories<Order>
    {
        List<Order> GetUserOrders(string userId);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
    }
}
