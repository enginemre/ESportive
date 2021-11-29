using SportiveOrder.Context;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class AddressRepositories : GenericRepository<Address>, IAddressRepository
    {
        public Address GetAdress(int companyId)
        {

            using var context = new SportiveOrderContext();
            var address = context.Address.Where(a => a.CompanyId == companyId).FirstOrDefault();
            return address;
        }
    }
}
