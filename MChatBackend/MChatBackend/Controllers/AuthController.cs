using MChatBackend.Core.DTO;
using MChatBackend.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MChatBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterService(req);
            if (result == null)
            {
                return BadRequest("Unable to create User ");
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.Login(req);
            if (result == null)
            {
                return BadRequest("User Not Found");
            }
            return Ok(result);

        }

        [HttpPost]
        [Authorize]
        public async Task<bool> Logout()
        {
            await _authService.Logout();
            return true;
        }
    }
}
