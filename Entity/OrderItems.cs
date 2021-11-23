using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Entity
{
    public class OrderItems
    {
        [Key]
        public int OrderItemsId { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "int")]
        [Required]
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
