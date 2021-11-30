using SportiveOrder.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Models
{
    public class Cart
    {
        public List<CartItem> products { get; set; }

        public decimal sum { get; set; }
    }
}
