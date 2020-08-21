using Microsoft.EntityFrameworkCore;

namespace BeltExam.Models
{
    public class BeltExamContext : DbContext //<~~ Add : DbContext
    {
        public BeltExamContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<AnActivity> AnActivities { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        
    }
}
// Migrations
// dotnet ef migrations add DbSetup
// dotnet ef database update