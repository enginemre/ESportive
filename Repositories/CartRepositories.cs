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

        public int AddCart(CartItem item)
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
                comingList.sum = item.quantity * item.product.SalePrice;
                _httpContextAccessor.HttpContext.Session.SetObject("cart", comingList);
                return 0;
            }
            else
            {
                foreach (var products in comingList.products)
                {
                    if(products.product.ProductId == item.product.ProductId)
                    {
                        return 1;
                    }
                }
                
                comingList.sum += (item.quantity*item.product.SalePrice) ;
                comingList.products.Add(item);
                _httpContextAccessor.HttpContext.Session.SetObject("cart", comingList);
                return 0;
            }
           
            
        }

        public Cart GetCartProducts()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
        }

        public void RemoveCart(int id)
        {
            var comingList = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            foreach (var item in comingList.products)
            {
                if(item.product.ProductId == id)
                {
                    comingList.sum -= (item.quantity * item.product.SalePrice);
                    comingList.products.Remove(item);
                    break;
                }
            }
            if(comingList.products.Count == 0)
            {
                comingList.sum = 0;
            }
            
            _httpContextAccessor.HttpContext.Session.SetObject("cart", comingList);
        }
        public void ClearCart()
        {
            Cart comingList = null;
            _httpContextAccessor.HttpContext.Session.SetObject("cart", comingList);
        }
       

       
    }
}
