using MChatBackend.Core.Domain.IdentityEntities;
using MChatBackend.Core.DTO;
using MChatBackend.Core.ReposetryContracts;
using MChatBackend.Core.ServiceContracts;
using MChatBackend.DTO;
using Microsoft.Extensions.Logging;

namespace MChatBackend.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthReposetry _auth;
        private readonly ILogger<AuthService> _logger;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthReposetry auth, ILogger<AuthService> logger, ITokenService tokenService)
        {
            _auth = auth;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResponse?> Login(LoginRequest req)
        {
            _logger.LogInformation("Service: Login started for {Email}", req.Email);

            var user = await _auth.UserLogin(req.Email!, req.Password!);

            if (user == null)
            {
                _logger.LogWarning("Service: Login failed for {Email}", req.Email);
                return null;
            }

            _logger.LogInformation("Service: Login successful for {Email}", req.Email);
            var tokenResult = await _tokenService.CreateToken(user);

            return new AuthenticationResponse
            {
                Id = user.Id,
                Email = user.Email,
                PersonName = user.PersonName,
                Token = tokenResult.Token,
                ExpirationTime = tokenResult.Expiration
            };
        }

        public async Task<AuthenticationResponse?> RegisterService(RegisterRequest req)
        {
            _logger.LogInformation("Service: Registration started for {Email}", req.Email);

            ApplicationUser user = new ApplicationUser
            {
                Email = req.Email,
                UserName = req.Email,
                PersonName = req.PersonName
            };

            var result = await _auth.Register(user, req.Password!);

            if (result == null)
            {
                _logger.LogWarning("Service: Registration failed for {Email}", req.Email);
                return null;
            }

            _logger.LogInformation("Service: Registration successful for {Email}", req.Email);
            var tokenResult = await _tokenService.CreateToken(result);

            return new AuthenticationResponse
            {
                Id=user.Id,
                Email = user.Email,
                PersonName = user.PersonName,
                Token = tokenResult.Token,
                ExpirationTime = tokenResult.Expiration
            };
            
        }

        public async Task<bool> Logout()
        {
            _logger.LogInformation("Service: Logout started");

            var result = await _auth.UserLogout();

            _logger.LogInformation("Service: Logout completed");

            return result;
        }
    }
}