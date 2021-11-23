using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SportiveOrder.Entity;

namespace SportiveOrder.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        public int CompanyId { get; set; }

        public Company UserCompany { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
