using Microsoft.AspNetCore.Mvc;
using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using System.Threading.Tasks;

namespace PetLuv.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            var result = await _authService.RegisterAsync(registerRequest);
            if (!result)
            {
                return BadRequest("Email already exists.");
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var result = await _authService.LoginAsync(loginRequest);
            if (result == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(result);
        }
    }
}