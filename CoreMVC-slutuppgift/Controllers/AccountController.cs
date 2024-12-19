using CoreMVC_slutuppgift.Models;
using CoreMVC_slutuppgift.Services;
using CoreMVC_slutuppgift.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC_slutuppgift.Controllers
{
    
    [Route("account")]
    public class AccountController : Controller
    {

        private readonly AccountService _accountService;

        // Konstruktor för att injicera AccountService
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        

        [HttpGet("register")]
        public IActionResult Register()
        {
            // Returnerar vyn för registrering med ett tomt modell
            return View(new RegisterViewModel());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Kontrollera om modellen är giltig
            if (!ModelState.IsValid)
            {
                return View(model); 
            }


            // Försöker registrera användaren via AccountService
            var result = await _accountService.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Index", "Car");
            }
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = string.Join("<br />", result.Errors.Select(e => e.Description));
                return View();
            }

            return View(model);

            
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Kontrollerar att e-post och lösenord är ifyllda
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Email and Password are required.";
                return View();
            }

            // Försöker logga in användaren via AccountService
            var result = await _accountService.LoginUserAsync(email, password);

            if (result)
            {
                var user = await _accountService.GetUserByEmailAsync(email);

                // Sparar användarens förnamn i sessionen
                HttpContext.Session.SetString("UserFirstName", user.FirstName);
                

                TempData["SuccessMessage"] = $"Welcome back, {user.FirstName}!";
                return RedirectToAction("Users");
            }

            TempData["ErrorMessage"] = "Invalid email or password.";
            return View();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Loggar ut användaren och rensar sessionen
            await _accountService.LogoutAsync();
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }

        [HttpGet("users")]
        [Authorize]
        public IActionResult Users()
        {
            // Hämtar alla användare och returnerar vyn för användarlistan
            var users = _accountService.GetAllUsers();
            return View(users);
        }
    }
}
