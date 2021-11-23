using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Entity
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(50)]
        public string StockCode { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(14)]
        public string Barcode { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(50)]
        public string Size { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(50)]
        public string Color { get; set; }
        [Column(TypeName = "int")]
        [Required]
        public int KDV { get; set; }
        [Column(TypeName = "int")]
        [Required]
        public int Stock { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(50)]
        public string ProductGroup { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<OrderItems> OrderItems { get; set; }
    }
}
