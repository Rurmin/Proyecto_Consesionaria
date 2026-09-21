using Domain.Entities;
using Infrastructure.Data;
using WebAPI.DTOs;

namespace WebAPI.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string RegistrarUsuario(RegisterDto request)
        {
            // 1. Validar si el correo ya existe
            if (_context.Usuarios.Any(u => u.Correo == request.Email))
            {
                return "El correo ya está registrado.";
            }

            // 2. Encriptar contraseña y mapear la entidad
            var nuevoUsuario = new Usuario
            {
                Nombre = request.Nombre,
                Correo = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            // 3. Guardar en base de datos
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            return "OK";
        }
    }
}