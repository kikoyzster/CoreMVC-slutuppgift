using CoreMVC_slutuppgift.Models;
using CoreMVC_slutuppgift.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreMVC_slutuppgift
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            


            // Add MVC services
            builder.Services.AddControllersWithViews();

            // Configure EF Core with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            })
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

            builder.Services.AddScoped<AccountService>();

            // Register CarService for dependency injection
            builder.Services.AddScoped<CarService>();

            builder.Services.AddSingleton<BrandService>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login"; 
                
            });
            var app = builder.Build();

            // Ensure database is created and seed data is added
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                
                dbContext.Database.EnsureCreated();

                
                // dbContext.Database.Migrate();
            }
            app.UseSession();
            // Stöd för Route-attribut på våra Action-metoder
            app.MapControllers();
            app.MapGet("/", (context) =>
            {
                context.Response.Redirect("/cars");
                return Task.CompletedTask;
            });

            
            app.UseAuthorization();

            app.Run();
        }
    }
}
