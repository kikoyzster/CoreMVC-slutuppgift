namespace CoreMVC_slutuppgift.Models
{
    public class CarService
    {
        private readonly List<Car> cars;

        public CarService()
        {
            cars = new List<Car>()
            {
                new Car {Id = 1 , Brand = "Toyta" , Model = "Corolla" , Year = 2020 },
                new Car {Id = 2 , Brand = "Honda" , Model = "Civic" , Year = 2019 },
                new Car {Id = 3 , Brand = "Volvo" , Model = "XC60" , Year = 2015 }
            };
        }

        public List<Car> GetAllCars()
        {
            return cars;
        }

        public void AddCar(Car car)
        {
            car.Id = cars.Count + 1;
            cars.Add(car);
        }
    }
}
