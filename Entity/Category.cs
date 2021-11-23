using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(75)]
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
