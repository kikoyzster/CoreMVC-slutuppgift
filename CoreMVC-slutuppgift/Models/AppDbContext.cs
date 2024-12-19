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

        // Konfigurering av datamodellen
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Kalla bas-klassens konfigurationer
            base.OnModelCreating(modelBuilder);

            // Seed-data för bilar
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020 },
                new Car { Id = 2, Brand = "Honda", Model = "Civic", Year = 2019 },
                new Car { Id = 3, Brand = "Volvo", Model = "XC60", Year = 2015 },
                new Car { Id = 4, Brand = "Ford", Model = "Focus", Year = 2018 },
                new Car { Id = 5, Brand = "BMW", Model = "3 Series", Year = 2021 },
                new Car { Id = 6, Brand = "Audi", Model = "A4", Year = 2020 },
                new Car { Id = 7, Brand = "Mercedes-Benz", Model = "C-Class", Year = 2019 },
                new Car { Id = 8, Brand = "Nissan", Model = "Altima", Year = 2018 },
                new Car { Id = 9, Brand = "Hyundai", Model = "Elantra", Year = 2022 },
                new Car { Id = 10, Brand = "Kia", Model = "Optima", Year = 2017 },
                new Car { Id = 11, Brand = "Chevrolet", Model = "Malibu", Year = 2020 },
                new Car { Id = 12, Brand = "Mazda", Model = "Mazda3", Year = 2021 },
                new Car { Id = 13, Brand = "Tesla", Model = "Model 3", Year = 2022 },
                new Car { Id = 14, Brand = "Volkswagen", Model = "Golf", Year = 2019 },
                new Car { Id = 15, Brand = "Subaru", Model = "Impreza", Year = 2020 },
                new Car { Id = 16, Brand = "Jeep", Model = "Cherokee", Year = 2018 },
                new Car { Id = 17, Brand = "Dodge", Model = "Charger", Year = 2021 },
                new Car { Id = 18, Brand = "Lexus", Model = "ES", Year = 2022 },
                new Car { Id = 19, Brand = "Acura", Model = "TLX", Year = 2019 },
                new Car { Id = 20, Brand = "Infiniti", Model = "Q50", Year = 2020 },
                new Car { Id = 21, Brand = "Porsche", Model = "Cayenne", Year = 2021 },
                new Car { Id = 22, Brand = "Land Rover", Model = "Discovery", Year = 2019 },
                new Car { Id = 23, Brand = "Jaguar", Model = "XE", Year = 2020 }
            );


        }
    }
}
