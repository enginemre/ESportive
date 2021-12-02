using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository,IProductRepository productRepository, UserManager<AppUser> userManager,IOrderItemsRepository orderItemsRepository)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _orderItemsRepository = orderItemsRepository;
            _productRepository = productRepository;
        }
        public IActionResult Indexs()
        {

            var list = _orderRepository.GetEntities();
            return View(list);
        }
        public IActionResult Details(string orderId)
        {
            var orderItemList = _orderItemsRepository.GetOrderProducts(orderId);
            foreach (var item in orderItemList)
            {
                item.Product = _productRepository.GetEntity(item.ProductId);
            }


            return View(orderItemList);  
        }
    }
}
