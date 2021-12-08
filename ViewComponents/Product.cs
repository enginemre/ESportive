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
        public IViewComponentResult Invoke(int? category_id, string price_sort, string searchString)
        {
            var list = new List<Entity.Product>();
            if (category_id.HasValue)
            {
                list = _productRepository.GetProducts((int)category_id);
                foreach (var item in list)
                {
                    if (item.ProductName != null && item.ProductName.Length > 20)
                    {
                        item.ProductName = item.ProductName.Substring(0, 20);
                    }
                }

            }
            else
            {
                list = _productRepository.GetEntities();
                foreach (var item in list)
                {
                    if (item.ProductName != null && item.ProductName.Length > 20)
                    {
                        item.ProductName = item.ProductName.Substring(0, 20);
                    }
                }
            }
            ViewData["CurrentFilter"] = searchString;
            switch (price_sort)
            {
                case "1":
                    ViewBag.price_sort = "1";
                    if (!String.IsNullOrEmpty(searchString))
                        return View(list.Where(pr => pr.ProductName.Contains(searchString)).OrderBy(pr => pr.SalePrice).ToList());
                    else
                        return View(list.OrderBy(pr => pr.SalePrice).ToList());
                case "2":
                    ViewBag.price_sort = "2";
                    if (!String.IsNullOrEmpty(searchString))
                        return View(list.Where(pr => pr.ProductName.Contains(searchString)).OrderByDescending(pr => pr.SalePrice).ToList());
                    else
                        return View(list.OrderByDescending(pr => pr.SalePrice).ToList());
                default:
                    ViewBag.price_sort = "";
                    if (!String.IsNullOrEmpty(searchString))
                        return View(list.Where(pr => pr.ProductName.Contains(searchString)).ToList());
                    else
                        return View(list);
            }

        }
    }
}
