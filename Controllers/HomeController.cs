using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using SportiveOrder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderItemsRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;


        public HomeController(IStringLocalizer<HomeController> stringLocalizer, ILogger<HomeController> logger, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IProductRepository productRepository, ICartRepository cartRepository,IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemsRepository;
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index(int? category_id)
        {
            ViewBag.category_id = category_id;
            return View(_productRepository.GetEntities());
        }
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetEntity(id);
            var quantity = 1;
            var cartItem = new CartItem { product = product, quantity = quantity };
            return View(cartItem);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async  Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        
        public IActionResult Cart(Cart carts)
        {
            var cart = _cartRepository.GetCartProducts();
              
            return View(cart);
        }

        public IActionResult AddCart(int id,int quantity)
        {
            var product = _productRepository.GetEntity(id);
            var cartItem = new CartItem { product = product, quantity = quantity };
            _cartRepository.AddCart(cartItem);
            TempData["Msg"] = "Selam";
            return RedirectToAction("Index");
        }




        [HttpPost]
        public async  Task<IActionResult> CreateOrder(Cart cart)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            cart = _cartRepository.GetCartProducts();
            var order = new Order
            {
                UserId = user.Id,
                User = user,
                CreatingAt = DateTime.Now,
            };
            var orderItemList = new  List<OrderItems>();
            _orderRepository.AddOrder(order);
            foreach (var item in cart.products)
            {
                OrderItems orderItem = new OrderItems
                {
                    Order = order,
                    OrderId = order.OrderId,
                    Product = item.product,
                    ProductId = item.product.ProductId,
                    Quantity = item.quantity
                };
                _orderItemRepository.AddOrderItems(orderItem);
                orderItemList.Add(orderItem);
            }
            order.OrderItems = orderItemList;
            _orderRepository.UpdateOrder(order);

            return RedirectToAction("Index", "Order", new { area = "" });
        }

        public IActionResult RemoveCart(int id)
        {
            _cartRepository.RemoveCart(id);

            return RedirectToAction("Cart","Home");
        }
    }
}
