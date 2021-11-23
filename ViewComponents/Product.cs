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
                return View(_productRepository.GetProducts((int)category_id));
            }
            return View(_productRepository.GetEntities());
        }
    }
}
