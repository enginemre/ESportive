using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportiveOrder.Areas.Identity.Data;
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
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public HomeController(ILogger<HomeController> logger, SignInManager<AppUser> signInManager, IProductRepository productRepository, ICartRepository cartRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index(int? category_id)
        {
            ViewBag.category_id = category_id;
            return View(_productRepository.GetEntities());
        }
        public IActionResult Details(int id)
        {

            return View(_productRepository.GetEntity(id));
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
    }
}
