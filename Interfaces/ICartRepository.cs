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
        int AddCart(CartItem cartItem);
        void RemoveCart(int id);
        Cart GetCartProducts();
        public void ClearCart();
    }
}
