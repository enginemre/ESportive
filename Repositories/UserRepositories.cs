using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Context;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class UserRepositories: GenericRepository<AppUser>, IUserRepository
    {
       
    }
}
