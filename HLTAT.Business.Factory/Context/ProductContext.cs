using Microsoft.EntityFrameworkCore;
using HLTAT.Business.Model;

namespace HLTAT.Business.Factory
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<ProductsModel> ProductsDbSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductsModel>(entity =>
            {
                entity.Property(p => p.ID).ValueGeneratedOnAdd();
            });
        }
    }
}
