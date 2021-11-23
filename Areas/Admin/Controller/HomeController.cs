using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Panel()
        {

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home",new { area = ""});
           
        }
    }
}
