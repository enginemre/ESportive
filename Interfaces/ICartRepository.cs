using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Interfaces
{
    public interface ICartRepository
    {
        void AddCart(Product product);
        void RemoveCart(Product product);
        List<Product> GetCartProducts();
    }
}
