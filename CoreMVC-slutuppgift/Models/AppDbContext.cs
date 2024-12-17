using Microsoft.EntityFrameworkCore;
using CoreMVC_slutuppgift.Models;

namespace CoreMVC_slutuppgift.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tabell för bilar
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
                new Car { Id = 2, Brand = "Honda", Model = "Civic", Year = 2019 },
                new Car { Id = 3, Brand = "Volvo", Model = "XC60", Year = 2015 }
            );
        }
    }
}
