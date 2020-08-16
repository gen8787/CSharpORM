using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Models
{
    public class ChefsNDishesContext : DbContext //<~~ Add : DbContext
    {
        public ChefsNDishesContext(DbContextOptions options) : base(options) { }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Chef> Chefs { get; set; }
    }
}

// Migrations
// dotnet ef migrations add dbSetup
// dotnet ef database update