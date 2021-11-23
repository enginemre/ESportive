using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Context;

[assembly: HostingStartup(typeof(SportiveOrder.Areas.Identity.IdentityHostingStartup))]
namespace SportiveOrder.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        // Bu sınıf identity için başlangıçta gerekli olan ayarlamaları yapıp contextimizi bağlamaktadır.
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
             
            });
        }



    
    }
}