using Microsoft.EntityFrameworkCore;
using CoreMVC_slutuppgift.Models;

namespace CoreMVC_slutuppgift.Models
{
    // Tjänstklass för att hantera bilrelaterade operationer
    public class CarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        // Hämtar alla bilar från databasen
        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }

        // Lägger till en ny bil i databasen
        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        // Hämtar en bil baserat på ID
        public Car GetCarById(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id);
        }

        // Uppdaterar information för en bil
        public void UpdateCar(int id, Car updatedCar)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
            {
                car.Brand = updatedCar.Brand;
                car.Model = updatedCar.Model;
                car.Year = updatedCar.Year;
                _context.SaveChanges();
            }
        }

        // Tar bort en bil från databasen
        public void DeleteCar(int id)
        {
            var car = GetCarById(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }
    }
}
