using CoreMVC_slutuppgift.Models;
using CoreMVC_slutuppgift.Services;
using CoreMVC_slutuppgift.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC_slutuppgift.Controllers
{
    [Route("cars")]
    public class CarController : Controller
    {
        private readonly CarService _carService;
        private readonly BrandService _brandService;

        public CarController(CarService carService ,BrandService brandService)
        {
            _carService = carService;
            _brandService = brandService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var carViewModles = _carService.GetAllCars().Select(car => new CarViewModel()
            {
               Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
            }).ToList();


            return View(carViewModles);

            
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var viewModel = new CarViewModel
            {
                AvailableBrands = _brandService.GetAvailableBrands()
    };
            return View(viewModel);
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
            carViewModel.AvailableBrands = _brandService.GetAvailableBrands();
            return View(carViewModel);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            var viewModel = new CarViewModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,    
                Year = car.Year,
                AvailableBrands= _brandService.GetAvailableBrands()
            };
            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id , CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                _carService.UpdateCar(id, new Car
                {
                    Brand = carViewModel.Brand,
                    Model = carViewModel.Model,
                    Year = carViewModel.Year ?? 0
                });
                return RedirectToAction("Index");
            }
            carViewModel.AvailableBrands= _brandService.GetAvailableBrands();
            return View(carViewModel);

        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction("Index");
        }


    }
}
