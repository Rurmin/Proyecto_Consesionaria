import axios from 'axios';

// 1. Configuración base usando variables de entorno
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL, // Ruta base dinámica según el entorno
  withCredentials: true // ESTO ES CRÍTICO: Permite que el navegador envíe y reciba cookies (JWT y CSRF)
});

// 2. Interceptor de Peticiones: Inyección del token Anti-CSRF
api.interceptors.request.use((config) => {
  // Los métodos que modifican datos requieren el token CSRF
  if (['post', 'put', 'delete', 'patch'].includes(config.method)) {
    // Buscar la cookie XSRF-TOKEN que nos enviará .NET
    const match = document.cookie.match(new RegExp('(^| )XSRF-TOKEN=([^;]+)'));
    if (match) {
      // Inyectarla en el header exacto que espera ASP.NET Core
      config.headers['X-XSRF-TOKEN'] = decodeURIComponent(match[2]);
    }
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});

// 3. Interceptor de Respuestas: Manejo de Errores Globales
api.interceptors.response.use(
  (response) => response,
  (error) => {
    // Si el backend devuelve un 401 (No Autorizado), limpiamos la sesión local
    if (error.response && error.response.status === 401) {
      localStorage.removeItem('pegasus_user');
      window.location.href = '/login';
    }
    
    // Aquí más adelante inyectaremos tu Toast de shadcn para mostrar errores globales
    console.error('API Error:', error.response?.data || error.message);
    
    return Promise.reject(error);
  }
);

export default api;