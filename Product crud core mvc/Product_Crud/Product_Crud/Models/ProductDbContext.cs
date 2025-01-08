using Microsoft.EntityFrameworkCore;

namespace Product_Crud.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public virtual DbSet<Product>? Products { get; set; }
        public virtual DbSet<Color>? Colors { get; set; }
        public virtual DbSet<Details>? Details { get; set; }

    }
}
