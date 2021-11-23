using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SportiveOrder.Models
{
    public class CRUDCategory
    {
        public int CategoryId { get; set; }
        [Display(Name ="Kategori İsmi")]
        [Required]
        [MaxLength(75, ErrorMessage = "En Fazla 75 Karakter olabilir.")]
        public string CategoryName { get; set; }
    }
}
