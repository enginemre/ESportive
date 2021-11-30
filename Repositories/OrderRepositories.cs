using SportiveOrder.Context;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class OrderRepositories : GenericRepository<Order>, IOrderRepository
    {
       public List<Order> GetUserOrders(string userId)
        {
            using var context = new SportiveOrderContext();
            return  context.Order.Where(o => o.UserId == userId).ToList();
        }
        public void AddOrder(Order order)
        {
            using var context = new SportiveOrderContext();
            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            context.Attach(order);
            context.Order.Add(order);
            context.SaveChanges();
        }
        public void UpdateOrder(Order order)
        {
            using var context = new SportiveOrderContext();
            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            context.Order.Update(order);
            context.SaveChanges();
        }
    }
}
