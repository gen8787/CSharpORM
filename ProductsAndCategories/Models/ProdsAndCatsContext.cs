using Microsoft.EntityFrameworkCore;

namespace ProductsAndCategories.Models
{
    public class ProdsAndCatsContext : DbContext //<~~ Add : DbContext
    {
        public ProdsAndCatsContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
    }
}
// Migrations
// dotnet ef migrations add DbSetup
// dotnet ef database update