using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[^<>&]*$", ErrorMessage = "Se han detectado caracteres no permitidos (<, >, &).")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [RegularExpression(@"^[^<>&]*$", ErrorMessage = "Se han detectado caracteres no permitidos (<, >, &).")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}