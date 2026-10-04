```markdown
# Concesionario Móvil - Sistema de Gestión

Sistema de gestión para concesionaria de vehículos desarrollado bajo una **Arquitectura Limpia (Clean Architecture)**, desacoplando completamente las reglas de negocio, la infraestructura de acceso a datos y la API REST en el backend, junto con una interfaz web responsiva y moderna en el frontend.

---

## 🛠️ Stack Tecnológico

### Backend
* **Lenguaje & Framework:** .NET 10 (C#) / ASP.NET Core Web API[cite: 19].
* **Arquitectura:** Clean Architecture (Capas: `Domain`, `Application`, `Infrastructure`, `WebAPI`)[cite: 19].
* **ORM:** Entity Framework Core (Enfoque Code-First)[cite: 19].
* **Base de Datos:** SQL Server (`PegasusDb`)[cite: 19].
* **Seguridad:** Hash de contraseñas con `BCrypt.Net` y gestión de secretos con `DotNetEnv`[cite: 19].

### Frontend
* **Framework:** Vue.js 3 (Composition API con `<script setup>`)[cite: 19].
* **Tooling & Build:** Vite.
* **Navegación:** Vue Router[cite: 19].
* **Diseño y Estilos:** Tailwind CSS + Componentes UI de `shadcn` (`Card`, `Input`, `Button`)[cite: 19].
* **Cliente HTTP:** Axios (configurado con baseURL adaptable para red local)[cite: 19].

---

## 📋 Requisitos Previos

Asegúrate de contar con las siguientes herramientas instaladas en tu entorno local:

* [.NET 10 SDK](https://dotnet.microsoft.com/)
* [SQL Server Express / LocalDB](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
* [Node.js (v18+) & npm](https://nodejs.org/)
* [Git](https://git-scm.com/)

---

## 🚀 Instalación y Configuración

### 1. Clonar el Repositorio
```bash
git clone [https://github.com/TU_USUARIO/Proyecto_Consesionaria.git](https://github.com/TU_USUARIO/Proyecto_Consesionaria.git)
cd Proyecto_Consesionaria

```

---

### 2. Configuración y Ejecución del Backend (.NET 10)

#### A. Configurar Variables de Entorno (`.env`)

Crea un archivo `.env` dentro de la carpeta `backend/WebAPI/` (o `WebAPI/` según la estructura de carpetas) para definir la cadena de conexión de forma segura sin exponer credenciales:

```env
DB_CONNECTION_STRING="Server=TU_SERVIDOR\SQLEXPRESS;Database=PegasusDb;Trusted_Connection=True;TrustServerCertificate=True;"

```

(Reemplaza `TU_SERVIDOR\SQLEXPRESS` por el nombre de tu instancia local de SQL Server).

#### B. Aplicar Migraciones en SQL Server

Abre una terminal en la raíz del proyecto backend y ejecuta el siguiente comando para construir la base de datos y la tabla `Usuarios`:

```bash
dotnet ef database update --project Infrastructure --startup-project WebAPI

```

#### C. Iniciar la Web API

Ejecuta el servidor enlazado a todas las interfaces de red para permitir la conexión desde dispositivos locales:

```bash
cd WebAPI
dotnet run --urls "[http://0.0.0.0:5000](http://0.0.0.0:5000)"

```

La API quedará escuchando peticiones en `http://localhost:5000` y a través de la IP local de tu máquina (ej. `http://192.168.10.87:5000`).

---

### 3. Configuración y Ejecución del Frontend (Vue 3)

#### A. Instalar Dependencias

Abre una nueva terminal, navega a la carpeta del proyecto frontend e instala los paquetes necesarios:

```bash
cd frontend
npm install

```

#### B. Iniciar Servidor de Desarrollo

```bash
npm run dev

```

Accede a la aplicación desde tu navegador en `http://localhost:5000`.

---

## 📡 Endpoints de la API (`AuthController`)

Base URL: `http://localhost:5000/api/auth`

| Método | Endpoint | Descripción | Body (JSON) |
| --- | --- | --- | --- |
| `POST` | `/api/auth/register` | Registro de nuevos usuarios con hash de contraseña. | `{ "username": "...", "email": "...", "password": "..." }` |
| `POST` | `/api/auth/login` | Autenticación y validación de credenciales. | `{ "username": "...", "password": "..." }` |

---

## 🔒 Seguridad e Interfaz Implementadas

* **Gestión de Secretos:** Aislamiento de cadenas de conexión con `DotNetEnv` (excluido de Git mediante `.gitignore`).


* **Seguridad de Credenciales:** Encriptación unidireccional de contraseñas con `BCrypt.Net`.


* **CORS Habilitado:** Comunicación fluida entre cliente Vue 3 y API .NET en red local.


* **Retroalimentación UX/UI:** Alertas integradas dentro de la interfaz (`RegisterView.vue` y `LoginView.vue`) eliminando cuadros `alert()` nativos del navegador.


* **Panel de Control:** Navegación modular mediante barra lateral (*Sidebar*) en `MenuView.vue`.
