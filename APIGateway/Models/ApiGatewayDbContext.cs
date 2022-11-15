using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Models
{
    public class ApiGatewayDbContext : DbContext
    {
        public ApiGatewayDbContext(DbContextOptions<ApiGatewayDbContext> options) : base(options) { }
        public DbSet<UserDetails> userDetails {get; set;}

        //public DbSet<RefreshTokendetails> refreshTokenDetails { get; set; }
    }
}
