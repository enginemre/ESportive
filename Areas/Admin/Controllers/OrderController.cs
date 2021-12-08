using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Entity;
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
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;

        public OrderController(IAddressRepository addressRepository,ICompanyRepository companyRepository, IOrderRepository orderRepository, IProductRepository productRepository, UserManager<AppUser> userManager, IOrderItemsRepository orderItemsRepository)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _orderItemsRepository = orderItemsRepository;
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
        }
        public async Task<IActionResult> Index()
        {

            var orders = new List<Order>();
            var orderIds = _orderRepository.GetEntities();
           
            foreach (var item in orderIds)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                var orderProducts = _orderItemsRepository.GetOrderProducts(item.OrderId);
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
            var orderItemList = _orderItemsRepository.GetOrderProducts(orderId);
            foreach (var item in orderItemList)
            {
                item.Product = _productRepository.GetEntity(item.ProductId);
            }


            return View(orderItemList);
        }
    }
}
