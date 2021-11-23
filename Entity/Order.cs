using SportiveOrder.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Entity
{
    public class Order
    {

        [Key]
        public string OrderId { get; set; }
        public String UserId { get; set; }
        [Timestamp]
        public DateTime CreatingAt { get; set; }

        public AppUser User { get; set; }

        public List<OrderItems> OrderItems { get; set; }
    }
}
