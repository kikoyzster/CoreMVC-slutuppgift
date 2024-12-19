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




            // Lägg till MVC-tjänster för att stödja controllers och vyer
            builder.Services.AddControllersWithViews();

            // Konfigurera EF Core för att använda SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Konfigurera Identity-tjänster för användarhantering och autentisering
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6; // Minimilängd för lösenord
                options.Password.RequireNonAlphanumeric = true; // Kräver specialtecken i lösenord
            })
                .AddEntityFrameworkStores<AppDbContext>() // Använd AppDbContext som datakälla
                .AddDefaultTokenProviders();

            // Lägg till AccountService som Scoped-tjänst för användarhantering
            builder.Services.AddScoped<AccountService>();

            // Registrera CarService för bilhantering
            builder.Services.AddScoped<CarService>();

            // Registrera BrandService som Singleton för tillgängliga bilmärken
            builder.Services.AddSingleton<BrandService>();

            // Aktivera sessioner med timeout och cookies
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Timeout för session
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Konfigurera inloggningsväg för cookies
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";  // Sätt standardvägen för inloggning

            });
            var app = builder.Build();

            // Säkerställ att databasen skapas och initieras med seed-data
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                      dbContext.Database.EnsureCreated();  // Skapar databasen om den inte redan finns
                   // dbContext.Database.Migrate();
            }

            // Aktivera sessionhantering
            app.UseSession();


            // Stöd för route-attribut i controllers
            app.MapControllers();

            // Omdirigera till /cars som standardväg
            app.MapGet("/", (context) =>
            {
                context.Response.Redirect("/cars");
                return Task.CompletedTask;
            });

            // Aktivera behörighetshantering
            app.UseAuthorization();

            app.Run();
        }
    }
}
