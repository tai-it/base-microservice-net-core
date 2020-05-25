namespace TVT.Catalog.Api.Domain.Entities.Context
{
    using Microsoft.EntityFrameworkCore;

    public class AppCatalogDbContext : DbContext
    {
        public AppCatalogDbContext(DbContextOptions<AppCatalogDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
