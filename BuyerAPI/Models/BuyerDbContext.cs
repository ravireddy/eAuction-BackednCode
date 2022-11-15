using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerAPI.Models
{
    public class BuyerDbContext : DbContext
    {
        public BuyerDbContext(DbContextOptions<BuyerDbContext> options) : base(options) { }
        public DbSet<ProductDetails> productDetails {get; set;}
        public DbSet<BidDetails> bidDetails { get; set; }
    }
}
