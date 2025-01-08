using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Product_34.Models
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext():base("ProductDb34")
        {
            
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Color> Colors{ get; set; }
        public virtual DbSet<Details> Details{ get; set; }
    }
}