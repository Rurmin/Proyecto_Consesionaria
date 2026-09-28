using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto request)
        {
            var resultado = _authService.RegistrarUsuario(request);

            if (resultado != "OK")
            {
                return BadRequest(new { message = resultado });
            }

            return Ok(new { message = "¡Cuenta creada con éxito!" });
        }
    }
}