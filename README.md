```markdown
# Proyecto Concesionaria

Sistema de gestión para concesionaria desarrollado bajo una arquitectura Clean Architecture, separando las responsabilidades en capas (Dominio, Infraestructura y WebAPI). 

Este proyecto cumple con los lineamientos de evaluación, integrando un backend robusto en .NET C# y un frontend progresivo en Vue.js.

## 🛠️ Stack Tecnológico
* **Backend:** .NET (C#) con ASP.NET Core Web API.
* **Arquitectura:** Clean Architecture.
* **Base de Datos:** SQL Server con Entity Framework Core (Code-First).
* **Frontend:** Vue.js 3 (Próximamente).

## 📋 Requisitos Previos
Para ejecutar este proyecto de manera local, necesitas tener instalado:
* [.NET SDK](https://dotnet.microsoft.com/) (Versión correspondiente al proyecto).
* [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) (Motor de base de datos).
* [Node.js y npm](https://nodejs.org/) (Para el entorno frontend).
* Git.

## 🚀 Instalación y Configuración (Backend)

### 1. Clonar el repositorio
```bash
git clone [https://github.com/TU_USUARIO/Proyecto_Consesionaria.git](https://github.com/TU_USUARIO/Proyecto_Consesionaria.git)
cd Proyecto_Consesionaria

```

### 2. Configurar Variables de Entorno (.env)

Por seguridad, las credenciales no están versionadas. Debes crear un archivo `.env` en la ruta `backend/WebAPI/` y configurar tu cadena de conexión local:

```env
DB_CONNECTION_STRING="Server=TU_SERVIDOR\SQLEXPRESS;Database=ConcesionariaDB;Trusted_Connection=True;TrustServerCertificate=True;"

```

*(Nota: Reemplaza `TU_SERVIDOR\SQLEXPRESS` con el nombre de tu instancia local de SQL Server).*

### 3. Aplicar Migraciones de Base de Datos

Abre tu terminal en la carpeta `backend` y ejecuta el siguiente comando para que Entity Framework Core construya la base de datos y sus tablas automáticamente:

```bash
dotnet ef database update --project Infrastructure --startup-project WebAPI

```

### 4. Ejecutar la API

Una vez configurada la base de datos, levanta el servidor backend:

```bash
cd WebAPI
dotnet run
