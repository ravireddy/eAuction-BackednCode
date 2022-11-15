using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SellerAPI.Models
{
    [Table("SellerDetails")]
    public class SellerDetails
    {
        [Key]
        public int SellerId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "required minimum 5 characters")]
        [MaxLength(30, ErrorMessage = "Allowed maximum 30 characters only")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "required minimum 3 characters")]
        [MaxLength(25, ErrorMessage = "Allowed maximum 25 characters only")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pin { get; set; }

        [Required]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [Column(TypeName = "numeric(10, 0)")]
        public Int64 Phone { get; set; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Invalid Email Id")]
        public string Email { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
