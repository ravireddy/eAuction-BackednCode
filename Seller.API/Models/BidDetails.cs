using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SellerAPI.Models
{
    [Table("BidDetails")]
    public class BidDetails
    {
        [Key]     
        public int BuyerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }

        [Column(TypeName = "numeric(10, 0)")]
        public Int64 Phone { get; set; }
        public string Email { get; set; }
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BidAmount { get; set; }

    }
}
