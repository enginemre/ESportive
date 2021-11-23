using Microsoft.AspNetCore.Identity;
using SportiveOrder.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Areas.Identity
{
    public  class IdentityInitiliaze
    {
        public static void CreateAdmin(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser appUser = new AppUser
            {
                UserName = "AdminT",

            };
            if (userManager.FindByNameAsync("AdminT").Result == null)
            {
                var result = userManager.CreateAsync(appUser, "Ae1234").Result;
            }
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityRole roleMember = new IdentityRole
                {
                    Name = "Member"
                };
                var resultRoleAdmin = roleManager.CreateAsync(role).Result;
                var resultRoleMember = roleManager.CreateAsync(roleMember).Result;
                var reslut2 = userManager.AddToRoleAsync(appUser, "Admin").Result;
            }
        }
    }
}
