using CoreMVC_slutuppgift.Models;
using CoreMVC_slutuppgift.Services;
using CoreMVC_slutuppgift.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMVC_slutuppgift.Controllers
{
    [Route("cars")]
    public class CarController : Controller
    {
        private readonly CarService _carService;
        private readonly BrandService _brandService;

        // Konstruktor för att injicera CarService och BrandService
        public CarController(CarService carService ,BrandService brandService)
        {
            _carService = carService;
            _brandService = brandService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            // Hämtar alla bilar och mappar dem till ViewModel för att visa i vyn
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
            // Förbereder en tom modell för att skapa en ny bil
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
                // Skapar en ny bil med data från ViewModel
                var car = new Car()
                {
                    
                    Brand = carViewModel.Brand,
                    Model = carViewModel.Model,
                    Year = carViewModel.Year ?? 0,
                   
                };

                _carService.AddCar(car);

                // Omdirigerar till Index-sidan efter att bilen har skapats
                return RedirectToAction("Index");
            }
            // Om modellen inte är giltig, ladda om sidan med tillgängliga märken
            carViewModel.AvailableBrands = _brandService.GetAvailableBrands();
            return View(carViewModel);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            // Hämtar bilen baserat på ID
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            // Förbereder ViewModel för redigering
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
                // Uppdaterar bilens data baserat på ID
                _carService.UpdateCar(id, new Car
                {
                    Brand = carViewModel.Brand,
                    Model = carViewModel.Model,
                    Year = carViewModel.Year ?? 0
                });
                // Omdirigerar till Index-sidan efter uppdateringen
                return RedirectToAction("Index");
            }
            // Om modellen inte är giltig, ladda om sidan med tillgängliga märken
            carViewModel.AvailableBrands= _brandService.GetAvailableBrands();
            return View(carViewModel);

        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            // Hämtar bilen som ska raderas baserat på ID
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                // Returnerar 404 om bilen inte finns
                return NotFound();
            }
            return View(car);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Tar bort bilen baserat på ID
            _carService.DeleteCar(id);
            // Omdirigerar till Index-sidan efter att bilen har tagits bort
            return RedirectToAction("Index");
        }

        [HttpGet("getcars")]
        public IActionResult GetCars()
        {
            // Returnerar alla bilar som JSON
            var cars = _carService.GetAllCars();
            return Json(cars);
        }

    }
}
