using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models

{
    public class WeddingPlannerContext : DbContext //<~~ Add : DbContext
    {
        public WeddingPlannerContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
    }
}
// Migrations
// dotnet ef migrations add DbSetup
// dotnet ef database update