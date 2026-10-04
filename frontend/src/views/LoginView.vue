<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '@/api/axios'
import { Card, CardHeader, CardTitle, CardContent, CardFooter } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'

const router = useRouter()
const isLoading = ref(false)
const errorMessage = ref('')

const credentials = ref({
  username: '', 
  password: ''
})

const procesarLogin = async () => {
  console.log("1. Botón nativo presionado. Datos capturados:", credentials.value);
  isLoading.value = true;
  errorMessage.value = '';
  
  try {
    console.log("2. Viajando al backend...");
    const response = await api.post('/auth/login', credentials.value);
    console.log("3. Respuesta del backend exitosa:", response.data);
    
    const usuario = response.data?.user || response.data?.User;
    if (usuario) {
      localStorage.setItem('pegasus_user', JSON.stringify(usuario));
    }
    router.push('/menu');
  } catch (error) {
    console.error("4. El backend rechazó la petición:", error.response?.data || error);
    if (error.response?.data?.message) {
      errorMessage.value = error.response.data.message;
    } else {
      errorMessage.value = 'Credenciales inválidas o error de conexión.';
    }
  } finally {
    isLoading.value = false;
  }
}
</script>

<template>
  <div class="flex min-h-screen items-center justify-center bg-slate-950 p-4 md:p-8 relative">
    <!-- Botón Volver responsivo: se ajusta la posición en tablet/escritorio -->
    <button 
      type="button"
      @click="router.push('/')" 
      class="absolute top-6 left-6 md:top-10 md:left-10 text-slate-400 hover:text-cyan-400 transition-all duration-300 flex items-center gap-2 font-bold text-sm z-50 hover:-translate-x-1 cursor-pointer"
    >
      <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
        <path d="m12 19-7-7 7-7"/><path d="M19 12H5"/>
      </svg>
      Volver al menú
    </button>

    <!-- Card Responsiva: Crece de max-w-sm (móvil) a max-w-md (tablet) y max-w-lg (escritorio). El padding también escala de p-4 a p-8 -->
    <Card class="w-full max-w-sm sm:max-w-md md:max-w-lg bg-[#171717] border-[#262626] rounded-[25px] shadow-[0_20px_25px_-5px_rgba(0,0,0,0.5)] hover:border-cyan-600 transition-colors duration-400 p-4 sm:p-6 md:p-8">
      <CardHeader class="pb-4">
        <CardTitle class="text-center text-white text-2xl md:text-3xl font-bold">Iniciar Sesión</CardTitle>
      </CardHeader>

      <CardContent>
        <p v-if="errorMessage" class="text-xs md:text-sm text-red-400 text-center font-semibold mb-4">
          {{ errorMessage }}
        </p>

        <form @submit.prevent="procesarLogin" class="space-y-4 md:space-y-5">
          <div class="flex items-center gap-2 rounded-[25px] px-4 py-3 bg-[#0a0a0a] shadow-[inset_2px_5px_10px_rgb(5,5,5)] border border-[#262626]">
            <svg class="h-[1.3em] w-[1.3em] fill-cyan-500 shrink-0" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16">
              <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
            </svg>
            <Input v-model="credentials.username" placeholder="Nombre de Usuario" type="text" required class="bg-transparent border-none outline-none text-[#d3d3d3] w-full p-0 h-auto focus-visible:ring-0 focus-visible:ring-offset-0 shadow-none text-sm md:text-base" />
          </div>

          <div class="flex items-center gap-2 rounded-[25px] px-4 py-3 bg-[#0a0a0a] shadow-[inset_2px_5px_10px_rgb(5,5,5)] border border-[#262626]">
            <svg class="h-[1.3em] w-[1.3em] fill-cyan-500 shrink-0" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16">
              <path d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z"/>
            </svg>
            <Input v-model="credentials.password" placeholder="Contraseña" type="password" required class="bg-transparent border-none outline-none text-[#d3d3d3] w-full p-0 h-auto focus-visible:ring-0 focus-visible:ring-offset-0 shadow-none text-sm md:text-base" />
          </div>

          <!-- Reemplazamos <Button> por la etiqueta nativa <button> -->
    <button 
      type="submit" 
      :disabled="isLoading" 
      class="w-full bg-cyan-600 hover:bg-cyan-500 hover:shadow-[0_0_15px_rgba(6,182,212,0.4)] text-white border-none font-bold rounded-xl py-4 transition-all duration-300 mt-4 text-sm md:text-base cursor-pointer"
    >
      {{ isLoading ? 'Iniciando...' : 'Iniciar Sesión' }}
    </button>
        </form>
      </CardContent>

      <CardFooter class="flex justify-center pt-2 pb-0">
        <button type="button" class="text-[#94a3b8] hover:text-cyan-500 text-sm md:text-base transition-colors cursor-pointer" @click="router.push('/registro')">
          ¿No tienes una cuenta? Regístrate aquí
        </button>
      </CardFooter>
    </Card>
  </div>
</template>