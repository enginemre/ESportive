using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.ViewComponents
{
    public class Product : ViewComponent
    {
        private readonly IProductRepository _productRepository;
        public Product(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IViewComponentResult Invoke(int? category_id)
        {
            if (category_id.HasValue)
            {
                var list = _productRepository.GetProducts((int)category_id);
                foreach (var item in list)
                {
                    if (item.ProductName != null && item.ProductName.Length > 20)
                    {
                        item.ProductName = item.ProductName.Substring(0, 20);
                    }
                }
                return View(list);
            }
            else
            {
                var list = _productRepository.GetEntities();
                foreach (var item in list)
                {
                    if (item.ProductName != null && item.ProductName.Length > 20)
                    {
                        item.ProductName = item.ProductName.Substring(0, 20);
                    }
                }
                return View(list);
            }
            
        }
    }
}
