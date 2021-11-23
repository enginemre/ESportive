using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Interfaces
{
    public interface IUserRepository : IGenericRepositories<AppUser>
    {
    }
}
