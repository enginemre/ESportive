using SportiveOrder.Context;
using SportiveOrder.Entity;
using SportiveOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Repositories
{
    public class ProductRepositories: GenericRepository<Product>,IProductRepository
    {
        public List<Product> GetProducts(int categoryID)
        {
            var context = new SportiveOrderContext();

            return context.Product.Where(b => b.CategoryId == categoryID).ToList();
        }
    }
}
