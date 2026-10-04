import { createApp } from 'vue'
import App from './App.vue'
import router from './router' // <-- 1. Importa el archivo de rutas que creaste
import './style.css' 

const app = createApp(App)

app.use(router) // <-- 2. Le enseña a Vue qué es el <router-view>
app.mount('#app')