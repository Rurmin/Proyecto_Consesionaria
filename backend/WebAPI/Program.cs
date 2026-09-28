using DotNetEnv;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar variables de entorno estables
Env.Load();

// 2. Obtener la cadena de conexión de forma segura
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

// 3. Registrar el DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<AuthService>();

// 4. Soporte indispensable para usar Controladores
builder.Services.AddControllers();

// 5. Configuración de Seguridad JWT
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
var key = Encoding.ASCII.GetBytes(jwtSecret!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        // Leer el JWT directamente desde la cookie HttpOnly
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["jwt"];
                return Task.CompletedTask;
            }
        };
    });

// 6. Definir la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// ... app.UseHttpsRedirection();

// 7. Aplicar la política CORS
app.UseCors("AllowFrontend");

// 8. Aplicar seguridad en ORDEN ESTRICTO
app.UseAuthentication(); // <-- 1. Identifica el token
app.UseAuthorization();  // <-- 2. Verifica permisos

app.MapControllers();
app.Run();