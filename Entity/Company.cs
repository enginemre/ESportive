using SportiveOrder.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Entity
{
    
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        public String UserId { get; set; }

        public AppUser User { get; set; }

        public Address CompanyAddress { get; set; }
        


    }
}
