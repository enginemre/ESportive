using Microsoft.AspNetCore.Http;
using SportiveOrder.CustomExtensions;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using SportiveOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class CartRepositories :ICartRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepositories(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddCart(CartItem item)
        {
            var comingList = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            
            if (comingList == null || comingList.products == null)
            {
                comingList = new Cart
                {
                    products = new List<CartItem>(),
                    sum = 0
                };
                comingList.products.Add(item);
            }
            else
            {
                comingList.sum += item.quantity;
                comingList.products.Add(item);
            }
            _httpContextAccessor.HttpContext.Session.SetObject("cart", comingList);
            
        }

        public Cart GetCartProducts()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
        }

        public void RemoveCart(CartItem item)
        {
            var comingList = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");

            comingList.products.Remove(item);
            _httpContextAccessor.HttpContext.Session.SetObject("cart", comingList);
        }

       

       
    }
}
