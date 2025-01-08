using Microsoft.EntityFrameworkCore;
using Product_Crud.Models;

namespace Product_Crud.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> products { get; set; }
        public DbSet<Color> colors { get; set; }
        public DbSet<Details> details { get; set; }

    }
}
