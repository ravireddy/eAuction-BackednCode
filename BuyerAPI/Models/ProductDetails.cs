using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyerAPI.Models
{
    [Table("Productdetails")]
    public class ProductDetails
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? StartingPrice { get; set; }
        public DateTime? BidEndDate { get; set; }
    }
    
}
