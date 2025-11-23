using Microsoft.EntityFrameworkCore;
using APIService.Models;
using MongoDB.Driver;

namespace APIService.Models
{
    internal class DrinksContext:DbContext
    {
        public DbSet<DrinkItem> drinks { get; init; }
        public DrinksContext(DbContextOptions<DrinksContext> options):base(options) {}
        public DbSet<DrinkItem> DrinkItems { get; set; } = null;
        public DbSet<APIService.Models.Supplier> Supplier { get; set; } = default!;
        public DbSet<APIService.Models.DrinksSales> DrinksSales { get; set; } = default!;

        public static DrinksContext Create(IMongoDatabase database) =>
            new(new DbContextOptionsBuiler<DrinksContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName
                .Options);
        public DrinksContext(DbContextOptions options)
            : base(options)
        {
            base.OnModelCreating(modelBuilder);
            ModelBuilder.Entity<DrinkItem>().ToCollection("drinks");
        }
    }
}
