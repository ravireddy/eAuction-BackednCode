using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerAPI.Models
{
    public class SellerDbContext : DbContext
    {
        public SellerDbContext(DbContextOptions<SellerDbContext> options) : base(options) { }
        public DbSet<ProductDetails> productDetails {get; set;}
        public DbSet<BidDetails> biddingDetails { get; set; }
        public DbSet<SellerDetails> sellerDetails { get; set; }

    }
}
