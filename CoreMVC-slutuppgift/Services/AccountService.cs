using CoreMVC_slutuppgift.Models;
using CoreMVC_slutuppgift.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC_slutuppgift.Services
{
    // Tjänstklass för att hantera kontorelaterade funktioner
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Metod för att registrera en ny användare
        public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser 
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate


            };
            // Skapar användaren med ett lösenord
            return await _userManager.CreateAsync(user, model.Password);
        }

        // Metod för att logga in en användare
        public async Task<bool> LoginUserAsync(string email, string password)
        {
            // Försöker logga in användaren med e-post och lösenord
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }

        // Metod för att logga ut användaren
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        // Hämtar alla användare från databasen
        public List<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        // Hämtar en användare baserat på e-post
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
