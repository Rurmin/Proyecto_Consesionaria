import axios from 'axios';

// Capturamos la URL y añadimos un respaldo (fallback) fuerte por si Vite no lee el .env
const baseURL = import.meta.env.VITE_API_URL || 'http://192.168.10.87:5000/api';
console.log('🔗 Axios apuntando a:', baseURL);

// 1. Configuración base
const api = axios.create({
  baseURL: baseURL, 
  withCredentials: true // ESTO ES CRÍTICO: Permite que el navegador envíe y reciba cookies
});

// 2. Interceptor de Peticiones: Inyección del token Anti-CSRF
api.interceptors.request.use((config) => {
  if (['post', 'put', 'delete', 'patch'].includes(config.method)) {
    const match = document.cookie.match(new RegExp('(^| )XSRF-TOKEN=([^;]+)'));
    if (match) {
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
    if (error.response && error.response.status === 401) {
      localStorage.removeItem('pegasus_user');
      window.location.href = '/login';
    }
    console.error('API Error:', error.response?.data || error.message);
    return Promise.reject(error);
  }
);

export default api;