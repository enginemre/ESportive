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
using SportiveOrder.Context;
using SportiveOrder.Interfaces;
using SportiveOrder.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddControllersWithViews();
            // Identity i�in razor pages ekleniyor
            services.AddRazorPages();
            services.AddDbContext<SportiveOrderContext>(options =>
                             options.UseSqlServer(
                                 Configuration.GetConnectionString("SportiveOrderContextConnection")));
            // Identity konfigrasyonlar� yap�l�yor.
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<SportiveOrderContext>();
            // Authorize i�in y�nlendirme ayarlar� yap�l�yor.
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
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


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            IdentityInitiliaze.CreateAdmin(userManager, roleManager);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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
