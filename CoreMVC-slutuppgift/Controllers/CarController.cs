using CoreMVC_slutuppgift.Models;
using CoreMVC_slutuppgift.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC_slutuppgift.Controllers
{
    [Route("cars")]
    public class CarController : Controller
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var carViewModles = _carService.GetAllCars().Select(car => new CarViewModel()
            {
               
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
            }).ToList();


            return View(carViewModles);

            
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                var car = new Car()
                {
                    Brand = carViewModel.Brand,
                    Model = carViewModel.Model,
                    Year = carViewModel.Year ?? 0,
                };

                _carService.AddCar(car);

                return RedirectToAction("Index");
            }
            return View(carViewModel);
        }
    }
}
