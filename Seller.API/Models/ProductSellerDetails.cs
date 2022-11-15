using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerAPI.Models
{
    public class ProductSellerDetails
    {
        public ProductDetails productDetails { get; set; }
        public SellerDetails sellerDetails { get; set; }
    }
}
