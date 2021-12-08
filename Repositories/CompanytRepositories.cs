using SportiveOrder.Context;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class CompanyRepositories : GenericRepository<Company>, ICompanyRepository
    {
        public Company GetCompanyWUserId(string id)
        {
            using var context = new SportiveOrderContext();
            return context.Company.Where(c => c.UserId == id).FirstOrDefault();
        }
       
    }
}
