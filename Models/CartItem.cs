using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Models
{
    public class CartItem
    {
        public Product product { get; set; }

        public int quantity { get; set; }
    }
}
