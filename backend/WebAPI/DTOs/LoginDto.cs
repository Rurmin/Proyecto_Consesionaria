using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [RegularExpression(@"^[^<>&]*$", ErrorMessage = "Se han detectado caracteres no permitidos (<, >, &).")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; } = string.Empty;
    }
}