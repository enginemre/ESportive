using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.Entity
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        [Column(TypeName = "nvarchar")]
        [Required]
        [MaxLength(30)]
        public string Province { get; set; }
        [Column(TypeName = "char")]
        [MaxLength(5)]
        public string PostCode { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Company Company { get; set; }

        public int CompanyId { get; set; }
    }
}
