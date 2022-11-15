using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SellerAPI.Models.eNum;

namespace SellerAPI.Models
{
    [Table("ProductDetails")]
    public class ProductDetails
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "required minimum 5 characters")]
        [MaxLength(30, ErrorMessage = "Allowed maximum 30 characters only")]
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }

        [Required]
        [CatergoryValidation(ErrorMessage = "Invalid category")]
        public string Category { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [StartingPriceValidation(ErrorMessage = "Starting Price should be greater than zero")]
        public decimal? StartingPrice { get; set; }

        [Column(TypeName = "date")]
        [BidEndDateValidation(ErrorMessage = "date should be future date")]
        public DateTime? BidEndDate { get; set; }


    }
    public class BidEndDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime _dateJoin = Convert.ToDateTime(value);
            if (_dateJoin.Date >= DateTime.Now.Date.AddDays(1))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
    public class StartingPriceValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = value.ToString();
            if (Convert.ToInt32(result) > 0)
            {
                return ValidationResult.Success;
            }

            else
            {
                return new ValidationResult(ErrorMessage);
            }

        }
    }
    public class CatergoryValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (System.Enum.IsDefined(typeof(eNum.Category), value.ToString()))
            {
                return ValidationResult.Success;
            }

            else
            {
                return new ValidationResult(ErrorMessage);
            }

        }
    }
}
