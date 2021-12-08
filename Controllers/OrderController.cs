using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using SportiveOrder.Models;
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
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IStringLocalizer<OrderController> _localizer;

        public OrderController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ICompanyRepository companyRepository, IAddressRepository addressRepository, IStringLocalizer<OrderController> stringLocalizer, ICartRepository cartRepository, IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository,IProductRepository product)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemsRepository;
            _productRepository = product;
            _localizer = stringLocalizer;
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
        }
        public async Task<IActionResult> Index()
        {
            var orders = new List<Order>();
            var user = await _userManager.GetUserAsync(User);
            var orderIds = _orderRepository.GetUserOrders(user.Id);
            ViewData["order_id"] = _localizer.GetString("order_id");
            ViewData["creating_at"] = _localizer.GetString("creating_at");
            ViewData["delivery_user"] = _localizer.GetString("delivery_user");
            foreach (var item in orderIds)
            {
               
                var orderProducts =  _orderItemRepository.GetOrderProducts(item.OrderId);
                foreach (var orderItem in orderProducts)
                {
                    orderItem.Product = _productRepository.GetEntity(orderItem.ProductId);
                }
                item.User = user;
                item.OrderItems = orderProducts;
                item.User.UserCompany = _companyRepository.GetCompanyWUserId(item.UserId);
                item.User.UserCompany.CompanyAddress = _addressRepository.GetEntity(item.User.CompanyId);
                item.UserId = user.Id;
    
            }
            return View(orderIds);
        }

        public IActionResult Details(string orderId)
        {
            List<OrderItems> list =_orderItemRepository.GetOrderProducts(orderId);
            foreach (var item in list)
            {
                item.Product = _productRepository.GetEntity(item.ProductId);
            }
            return View(list);
        }
    }
}
