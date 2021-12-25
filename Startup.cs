using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportiveOrder.Areas.Identity;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Areas.Identity.Pages;
using Westwind.AspNetCore.LiveReload;
using SportiveOrder.Context;
using SportiveOrder.Interfaces;
using SportiveOrder.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Reflection;
using SportiveOrder.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using SportiveOrder.Areas.Identity.Pages.Account;
using SportiveOrder.CustomExtensions;

namespace SportiveOrder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var supportedCultures = new List<CultureInfo>
            {
                 new CultureInfo("tr-TR"),
                 new CultureInfo("en-US")

            };
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";

            });
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                opt.DefaultRequestCulture = new RequestCulture("tr-TR");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;
                opt.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });

            services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization().AddRazorRuntimeCompilation();


            // http ye sepet reposundan ulaþýlmasý saðlanýyor.
            services.AddHttpContextAccessor();
            // Identity için razor pages ekleniyor
            services.AddRazorPages();
            services.AddDbContext<SportiveOrderContext>(options =>
                             options.UseSqlServer(
                                 Configuration.GetConnectionString("SportiveOrderContextConnection")));
            // Sepet için session ekleniyor
            services.AddSession();
            // Swagger ekleniyor
            services.AddSwaggerDocument();
            // Hot Reload
            services.AddLiveReload(config =>
            {
                config.LiveReloadEnabled = true;
                config.ClientFileExtensions = ".cshtml,.css,.js,.htm,.html,.ts,.razor,.custom";
                config.ServerRefreshTimeout = 3000;
                //config.FolderToMonitor = Path.GetFullname(Path.Combine(Env.ContentRootPath,"..")) ;
            });


            // Identity konfigrasyonlarý yapýlýyor
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 2;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<SportiveOrderContext>().AddErrorDescriber<TurkishIdentityErrorDescriber>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);
            // Authorize için yönlendirme ayarlarý yapýlýyor.
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Identity/Account/Login");
                opt.Cookie.Name = "ESportive";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });
            // Dependecy Injection
            services.AddScoped<ICartRepository, CartRepositories>();
            services.AddScoped<ICategoryRepository, CategoryRepositories>();
            services.AddScoped<IProductRepository, ProductRepositories>();
            services.AddScoped<IAddressRepository, AddressRepositories>();
            services.AddScoped<ICompanyRepository, CompanyRepositories>();
            services.AddScoped<IOrderRepository, OrderRepositories>();
            services.AddScoped<IUserRepository, UserRepositories>();
            services.AddScoped<IOrderItemsRepository, OrderItemsRepositories>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Localization
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            options.Value.SetDefaultCulture("tr-TR");
            app.UseRequestLocalization(options.Value);
            // Api documentation
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            IdentityInitiliaze.CreateAdmin(userManager, roleManager);
            
            app.UseRouting();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseLiveReload();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas", pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
