using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Areas.Identity.Pages.Account;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using SportiveOrder.Models;

namespace SportiveOrder.Areas.Admin.Controller
{
    [Area("Admin")]
    public class UserController: Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICompanyRepository _companyRepository;
        public UserController(IUserRepository userRepository, IAddressRepository addressRepository, ICompanyRepository companyRepository,  UserManager<AppUser> userManager, SignInManager<AppUser> signInManager )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _addressRepository = addressRepository;
            _companyRepository = companyRepository;
        }
        public IActionResult Index()
        {
            
            return View(_userRepository.GetUsers());
        }

        public IActionResult Create()
        {
            return View(new RegisterModel.InputModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel.InputModel model)
        {
            if (ModelState.IsValid)
            {
                var company = new Company
                {
                    CompanyName = model.CompanyName,
                };
                _companyRepository.Add(company);
                var user = new AppUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.Phone,CompanyId = company.CompanyId };
                var result = await _userManager.CreateAsync(user, model.Password);
                var address = new Address
                {
                    City = model.City,
                    Province = model.Province,
                    PostCode = model.PostCode,
                    CompanyId = company.CompanyId,
                    Company = company
                };
                company.User = user;
                company.UserId = user.Id;
                company.CompanyAddress = address;
                user.UserCompany = company;
                address.Company = company;
                address.CompanyId = company.CompanyId;
                var updatedResult =  _userManager.UpdateAsync(user).Result;

               
                if (updatedResult.Succeeded && result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "Member");
                    ViewBag.msg = $"{user.UserName} + Başarıyla Oluşturuldu";
                    return RedirectToAction("Index", "User", new { area = "Admin" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string UserId)
        {
            var user =  _userManager.FindByIdAsync(UserId).Result;
            var role = _userManager.GetRolesAsync(user).Result;
            var company = _companyRepository.GetEntity(user.CompanyId);
            var address = _addressRepository.GetAdress(company.CompanyId);
            company.CompanyAddress = address;
            user.UserCompany = company;
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                CRUDUser CRUDuser = new CRUDUser
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    CompanyId = user.CompanyId,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    RoleName = role[0],
                    UserCompany = user.UserCompany
              
                };
                return View(CRUDuser);
            }
            

        }

        [HttpPost]
        public IActionResult Edit(CRUDUser CRUDUser)
        {
            var orgUser = _userManager.FindByIdAsync(CRUDUser.UserId).Result;
            var company = _companyRepository.GetEntity(orgUser.CompanyId);
            var address = _addressRepository.GetAdress(company.CompanyId);
            if (ModelState.IsValid)
            {
                orgUser.CompanyId = orgUser.CompanyId;
                orgUser.Email = CRUDUser.Email;
                orgUser.Id = CRUDUser.UserId;
                orgUser.PhoneNumber = CRUDUser.PhoneNumber;
                orgUser.UserCompany = CRUDUser.UserCompany;
                orgUser.UserCompany.CompanyId = company.CompanyId;
                orgUser.UserCompany.UserId = CRUDUser.UserId;
                orgUser.UserCompany.User = orgUser;
                orgUser.UserCompany.CompanyAddress.AddressId = address.AddressId;
                orgUser.UserCompany.CompanyAddress.CompanyId = company.CompanyId;
                orgUser.UserCompany.CompanyAddress.Company = company;
                orgUser.UserName = CRUDUser.UserName;

                _userRepository.Update(orgUser);
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }

            return View(CRUDUser);
        }

        public async Task<IActionResult> Delete(string UserId)
        {
            var user = _userManager.FindByIdAsync(UserId).Result;
            var logins = await _userManager.GetLoginsAsync(user);
            var rolesForUser = await _userManager.GetRolesAsync(user);

            foreach (var login in logins)
            {
                var result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                if (result.Succeeded)
                    break;
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index", "User", new { area = "Admin" });
            
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                if(user == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            return View(model);
        }
    }
}
