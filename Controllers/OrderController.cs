using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Controllers
{
    public class OrderController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderItemsRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ICartRepository cartRepository, IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemsRepository;
        }
        public async Task<IActionResult> Index()
        {
            var orders = new List<Order>();
            var user = await _userManager.GetUserAsync(User);
            var orderIds = _orderRepository.GetUserOrders(user.Id);
            foreach (var item in orderIds)
            {
                var orderProducts =  _orderItemRepository.GetOrderProducts(item.OrderId);
                item.OrderItems = orderProducts;
                item.User = user;
                item.UserId = user.Id;
            }
            return View(orderIds);
        }
    }
}
