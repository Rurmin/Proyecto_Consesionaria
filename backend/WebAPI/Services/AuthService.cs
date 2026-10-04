using System;
using System.Linq;
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
            if (_context.Usuarios.Any(u => u.Email == request.Email))
            {
                return "El correo ya está registrado.";
            }

            // 2. Crear entidad con la fecha actual
            var nuevoUsuario = new Usuario
            {
                NombreCompleto = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Rol = "Vendedor",
                FechaRegistro = DateTime.Now
            };

            // 3. Guardar en base de datos
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            return "OK";
        }

        public string IniciarSesion(LoginDto request)
        {
            // Buscar usuario por correo o por NombreCompleto (Username)
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == request.Username || u.NombreCompleto == request.Username);

            if (usuario == null)
            {
                return "Usuario no encontrado";
            }

            // Verificar hash de contraseña
            bool esValida = BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash);
            if (!esValida)
            {
                return "Credenciales inválidas";
            }

            return "OK";
        }
    }
}