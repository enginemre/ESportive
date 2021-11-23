using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Models
{
    public class CRUDProduct
    {
        public int Id { get; set; }

        [Display(Name = "Ürün İsmi")]
        [Required(ErrorMessage = "Ürün İsmi gereklidir.")]
        public string Name { get; set; }
        [Display(Name = "Stok Kodu")]
        [Required(ErrorMessage = "Stok Kodu gereklidir.")]
        public string StockCode { get; set; }
        [Display(Name = "Ürün Barkod")]
        [Required(ErrorMessage = "Barkod  gereklidir.")]
        public string Barcode { get; set; }
        [Display(Name = "Ürün Bedeni")]
        [Required(ErrorMessage = "Beden  gereklidir.")]
        public string Size { get; set; }
        [Display(Name = "Ürün Rengi")]
        [Required(ErrorMessage = "Renk  gereklidir.")]
        public string Color { get; set; }
        [Display(Name = "KDV")]
        [Range(1, 100, ErrorMessage = "1 ile 100 arasında olmalıdır.")]
        [Required(ErrorMessage = "Ürünün KDV' si  gereklidir.")]
        public int KDV { get; set; }

        [Display(Name = "Stok Miktarı")]
        [Required(ErrorMessage = "Ürünün Stok bilgisi  gereklidir.")]
        public int Stock { get; set; }
        [Display(Name = "Satış Fiyatı")]
        [Required(ErrorMessage = "Ürünün Satış Fiyatı gereklidir.")]
        public decimal SalePrice { get; set; }
        [Display(Name = "Alış Fiyatı")]
        [Required(ErrorMessage = "Ürünün Alış Fiyatı gereklidir.")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Ürün Grubu")]
        [Required(ErrorMessage = "Ürünün ürün grubu  gereklidir.")]
        public string ProductGroup { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori Seçilmek Zorundadır.")]
        public int CategoryId { get; set; }
    }
}
