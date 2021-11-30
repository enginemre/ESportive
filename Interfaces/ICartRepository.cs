using SportiveOrder.Entity;
using SportiveOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Interfaces
{
    public interface ICartRepository
    {
        void AddCart(CartItem cartItem);
        void RemoveCart(CartItem cartItem);
        Cart GetCartProducts();
    }
}
