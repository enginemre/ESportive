﻿using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Interfaces
{
    public interface IOrderItemsRepository : IGenericRepositories<OrderItems>
    {
        List<OrderItems> GetOrderProducts(string orderId);
        void AddOrderItems(OrderItems orderItems);
    }
}
