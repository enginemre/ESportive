using Microsoft.AspNetCore.Http;
using SportiveOrder.CustomExtensions;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
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

        public void AddCart(Product product)
        {
            var comingList = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("cart");

            if (comingList == null)
            {
                comingList = new List<Product>();
                comingList.Add(product);
            }
            else
            {
                comingList.Add(product);
            }
            _httpContextAccessor.HttpContext.Session.SetObject("cart", comingList);
        }

        public List<Product> GetCartProducts()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("cart");
        }

        public void RemoveCart(Product product)
        {
            var comingList = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("cart");

            comingList.Remove(product);
            _httpContextAccessor.HttpContext.Session.SetObject("cart", comingList);
        }

       

       
    }
}
