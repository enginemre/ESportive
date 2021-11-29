using SportiveOrder.Areas.Identity.Data;
using SportiveOrder.Context;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using SportiveOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class UserRepositories: GenericRepository<AppUser>, IUserRepository
    {
        public List<CRUDUser> GetUsers()
        {
            var context = new SportiveOrderContext();
            var query = from usr in context.Users
                        join usrRole in context.UserRoles on usr.Id equals usrRole.UserId
                        join role in context.Roles on usrRole.RoleId equals role.Id
                        select new CRUDUser
                        {
                            UserId = usr.Id,
                            CompanyId = usr.CompanyId,
                            UserName = usr.UserName,
                            UserCompany = usr.UserCompany,
                            Email = usr.Email,
                            PhoneNumber = usr.PhoneNumber,
                            RoleName = role.Name,
                        };
            return query.ToList();
            
        }

        public AppUser GetUser(string id)
        {
            var context = new SportiveOrderContext();
            return context.Set<AppUser>().Find(id);
        }
    }
}
