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

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

         

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

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Email and Password are required.";
                return View();
            }

            var result = await _accountService.LoginUserAsync(email, password);

            if (result)
            {
                var user = await _accountService.GetUserByEmailAsync(email);


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
            await _accountService.LogoutAsync();
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }

        [HttpGet("users")]
        [Authorize]
        public IActionResult Users()
        {
            var users = _accountService.GetAllUsers();
            return View(users);
        }
    }
}
