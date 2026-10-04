using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define la ruta base: /api/auth
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
            if (resultado == "OK")
            {
                return Ok(new { message = "Usuario registrado con éxito" });
            }
            return BadRequest(new { message = resultado });
        }

        [HttpPost("login")] // <-- Este endpoint es el que genera el 404
        public IActionResult Login([FromBody] LoginDto request)
        {
            var resultado = _authService.IniciarSesion(request);
            if (resultado == "Credenciales inválidas" || resultado == "Usuario no encontrado")
            {
                return BadRequest(new { message = resultado });
            }

            return Ok(new { message = "Inicio de sesión exitoso", token = resultado });
        }
    }
}