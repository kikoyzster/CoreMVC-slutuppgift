using Microsoft.EntityFrameworkCore;
using CoreMVC_slutuppgift.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CoreMVC_slutuppgift.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tabell för bilar
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
                new Car { Id = 2, Brand = "Honda", Model = "Civic", Year = 2019 },
                new Car { Id = 3, Brand = "Volvo", Model = "XC60", Year = 2015 }
            );

//            modelBuilder.Entity<IdentityRole>().HasData(
//    new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
//    new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
//);
        }
    }
}
