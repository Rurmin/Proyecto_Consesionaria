import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './style.css' // Mantén la importación de tus estilos (Tailwind)

const app = createApp(App)

app.use(router)
app.mount('#app')