using Microsoft.EntityFrameworkCore;
using CoreMVC_slutuppgift.Models;

namespace CoreMVC_slutuppgift.Models
{
    public class CarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }

        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public Car GetCarById(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id);
        }
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
