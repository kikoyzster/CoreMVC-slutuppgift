using CoreMVC_slutuppgift.Models;

namespace CoreMVC_slutuppgift
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register CarService for dependency injection
            builder.Services.AddSingleton<CarService>();

            // Add MVC services
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // St�d f�r Route-attribut p� v�ra Action-metoder
            app.MapControllers();
            app.MapGet("/", (context) =>
            {
                context.Response.Redirect("/cars");
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}
