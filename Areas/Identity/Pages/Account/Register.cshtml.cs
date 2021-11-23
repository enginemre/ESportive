using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Entity;

namespace SportiveOrder.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Kullanıcı Adı")]
            public string UserName { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Telefon Numarası")]
            public string Phone { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Şirket İsmi")]
            public string CompanyName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "İl")]
            public string City { get; set; }

         
            [DataType(DataType.Text)]
            [Display(Name = "İlçe")]
            public string Province { get; set; }

            [DataType(DataType.Text)]
            [MaxLength(5)]
            [Display(Name = "Posta Kodu")]
            public string PostCode { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "E-Posta")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Şifre en az 5 karakter ", MinimumLength = 5)]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Şifre Onayla")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var address = new Address
                {
                    City = Input.City,
                    Province = Input.Province,
                    PostCode = Input.PostCode
                };
                var company = new Company
                {
                    CompanyName = Input.CompanyName,
                };
                address.Company = company;
                company.CompanyAddress = address;
                var user = new AppUser { UserName = Input.UserName, Email = Input.Email, PhoneNumber = Input.Phone};
                company.User = user;
                user.UserCompany = company;
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "Member");
                    _logger.LogInformation("Kullanıcı Oluşturuldu");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
