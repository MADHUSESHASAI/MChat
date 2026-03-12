using MChatBackend.Core.Domain.IdentityEntities;
using MChatBackend.Core.DTO;
using MChatBackend.Core.ReposetryContracts;
using MChatBackend.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MChatBackend.Infrastrecture.Reposetries
{
    public class AuthReposetry : IAuthReposetry
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthReposetry> _logger;

        public AuthReposetry(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AuthReposetry> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<ApplicationUser?> Register(ApplicationUser user, string password)
        {
            _logger.LogInformation("Repository: Registration started for {Email}", user.Email);

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _logger.LogInformation("Repository: Registration successful for {Email}", user.Email);

                return await UserLogin(user.UserName!, password);
            }

            _logger.LogWarning("Repository: Registration failed for {Email}. Errors: {Errors}",
                user.Email,
                string.Join(",", result.Errors.Select(e => e.Description)));

            return null;
        }

        public async Task<ApplicationUser?> UserLogin(string username, string password)
        {
            _logger.LogInformation("Repository: Login attempt for {Email}", username);

            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(username);

                _logger.LogInformation("Repository: Login successful for {Email}", username);

                return user;
            }

            _logger.LogWarning("Repository: Login failed for {Email}", username);
            return null;
        }

        public async Task<bool> UserLogout()
        {
            _logger.LogInformation("Repository: Logout requested");

            await _signInManager.SignOutAsync();

            _logger.LogInformation("Repository: Logout successful");
            return true;
        }
    }
}