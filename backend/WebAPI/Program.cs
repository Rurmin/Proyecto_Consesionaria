using DotNetEnv;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar variables de entorno estables
Env.Load();

// 2. Obtener la cadena de conexión de forma segura
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

// 3. Registrar el DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<AuthService>();

// 1. Definir la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Puerto por defecto de Vite 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // JWT en cookies más adelante
    });
});

var app = builder.Build();

// ... app.UseHttpsRedirection();

// 2. Aplicar la política (DEBE ir antes de UseAuthorization)
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();
app.Run();