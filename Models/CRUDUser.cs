using Microsoft.EntityFrameworkCore;
using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Models
{
    [Keyless]
    public class CRUDUser
    {
        public string UserId { get; set; }
        public int CompanyId { get; set; }
        public  string PhoneNumber { get; set; }

        public string RoleName { get; set; }

        public  string UserName { get; set; }
        public  string Email { get; set; }
        public Company UserCompany { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
