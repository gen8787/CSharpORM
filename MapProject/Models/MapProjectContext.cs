using Microsoft.EntityFrameworkCore;

namespace MapProject.Models
{
    public class MapProjectContext : DbContext //<~~ Add : DbContext
    {
        public MapProjectContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Leg> Legs { get; set; }
        public DbSet<Tour> Tours { get; set; }

        // public DbSet<Plan> Plans { get; set; }
    }
}
// Migrations
// dotnet ef migrations add DbSetup
// dotnet ef database update