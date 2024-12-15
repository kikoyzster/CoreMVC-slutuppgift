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

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
