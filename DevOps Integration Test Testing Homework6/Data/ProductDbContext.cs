using Entities;
using Microsoft.EntityFrameworkCore;

namespace DevOps_Integration_Test_Testing_Homework6.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    }
}
