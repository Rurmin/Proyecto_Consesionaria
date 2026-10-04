<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '@/api/axios'
import { Card, CardHeader, CardTitle, CardContent, CardFooter } from '@/components/ui/card'
import { Input } from '@/components/ui/input'

const username = ref('')
const email = ref('')
const password = ref('')
const confirmPassword = ref('')
const errorMessage = ref('')
const successMessage = ref('')
const isLoading = ref(false)
const router = useRouter()

const handleRegister = async () => {
  errorMessage.value = ''
  successMessage.value = ''

  if (password.value !== confirmPassword.value) {
    errorMessage.value = 'Las contraseñas no coinciden'
    return
  }

  isLoading.value = true

  try {
    const response = await api.post('/auth/register', {
      email: email.value,
      password: password.value,
      nombre: username.value,
      username: username.value
    })

    // Mensaje en verde debajo del título
    successMessage.value = response.data.message || '¡Cuenta creada con éxito!'

    // Redirección al login tras 1.5 segundos
    setTimeout(() => {
      router.push('/login')
    }, 1500)

  } catch (error) {
    if (error.response?.data) {
      const data = error.response.data
      if (data.message) {
        errorMessage.value = data.message
      } else if (data.errors) {
        const primerError = Object.values(data.errors)[0]
        errorMessage.value = Array.isArray(primerError) ? primerError[0] : 'Datos de registro inválidos'
      } else {
        errorMessage.value = 'Error al registrar el usuario'
      }
    } else {
      errorMessage.value = 'Error de conexión con el servidor.'
    }
  } finally {
    isLoading.value = false
  }
}
</script>

<template>
  <div class="flex min-h-screen items-center justify-center bg-slate-950 p-4 md:p-8 relative">
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

    <Card class="w-full max-w-sm sm:max-w-md md:max-w-lg bg-[#171717] border-[#262626] rounded-[25px] shadow-[0_20px_25px_-5px_rgba(0,0,0,0.5)] hover:border-cyan-600 transition-colors duration-400 p-4 sm:p-6 md:p-8">
      <CardHeader class="pb-4">
        <CardTitle class="text-center text-white text-2xl md:text-3xl font-bold">Crear Cuenta</CardTitle>
      </CardHeader>

      <CardContent>
        <!-- Mensajes de Estado -->
        <p v-if="errorMessage" class="text-xs md:text-sm text-red-400 text-center font-semibold mb-4">
          {{ errorMessage }}
        </p>

        <p v-if="successMessage" class="text-xs md:text-sm text-emerald-400 text-center font-semibold mb-4">
          {{ successMessage }}
        </p>

        <form @submit.prevent="handleRegister" class="space-y-4 md:space-y-5">
          <div class="flex items-center gap-2 rounded-[25px] px-4 py-3 bg-[#0a0a0a] shadow-[inset_2px_5px_10px_rgb(5,5,5)] border border-[#262626]">
            <svg class="h-[1.3em] w-[1.3em] fill-cyan-500 shrink-0" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16">
              <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
            </svg>
            <Input v-model="username" placeholder="Nombre de usuario" type="text" required class="bg-transparent border-none outline-none text-[#d3d3d3] w-full p-0 h-auto focus-visible:ring-0 focus-visible:ring-offset-0 shadow-none text-sm md:text-base" />
          </div>

          <div class="flex items-center gap-2 rounded-[25px] px-4 py-3 bg-[#0a0a0a] shadow-[inset_2px_5px_10px_rgb(5,5,5)] border border-[#262626]">
            <svg class="h-[1.3em] w-[1.3em] fill-cyan-500 shrink-0" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16">
              <path d="M13.106 7.222c0-2.967-2.249-5.032-5.482-5.032-3.35 0-5.646 2.318-5.646 5.702 0 3.493 2.235 5.708 5.762 5.708.862 0 1.689-.123 2.304-.335v-.862c-.43.199-1.354.328-2.29.328-2.926 0-4.813-1.88-4.813-4.798 0-2.844 1.921-4.881 4.594-4.881 2.735 0 4.608 1.688 4.608 4.156 0 1.682-.554 2.769-1.416 2.769-.492 0-.772-.28-.772-.76V5.206H8.923v.834h-.11c-.266-.595-.881-.964-1.6-.964-1.4 0-2.378 1.162-2.378 2.823 0 1.737.957 2.906 2.379 2.906.8 0 1.415-.39 1.709-1.087h.11c.081.67.703 1.148 1.503 1.148 1.572 0 2.57-1.415 2.57-3.643zm-7.177.704c0-1.197.54-1.907 1.456-1.907.93 0 1.524.738 1.524 1.907S8.308 9.84 7.371 9.84c-.895 0-1.442-.725-1.442-1.914z"/>
            </svg>
            <Input v-model="email" placeholder="Correo electrónico" type="email" required class="bg-transparent border-none outline-none text-[#d3d3d3] w-full p-0 h-auto focus-visible:ring-0 focus-visible:ring-offset-0 shadow-none text-sm md:text-base" />
          </div>

          <div class="flex items-center gap-2 rounded-[25px] px-4 py-3 bg-[#0a0a0a] shadow-[inset_2px_5px_10px_rgb(5,5,5)] border border-[#262626]">
            <svg class="h-[1.3em] w-[1.3em] fill-cyan-500 shrink-0" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16">
              <path d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z"/>
            </svg>
            <Input v-model="password" placeholder="Contraseña" type="password" required class="bg-transparent border-none outline-none text-[#d3d3d3] w-full p-0 h-auto focus-visible:ring-0 focus-visible:ring-offset-0 shadow-none text-sm md:text-base" />
          </div>

          <div class="flex items-center gap-2 rounded-[25px] px-4 py-3 bg-[#0a0a0a] shadow-[inset_2px_5px_10px_rgb(5,5,5)] border border-[#262626]">
            <svg class="h-[1.3em] w-[1.3em] fill-cyan-500 shrink-0" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16">
              <path d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z"/>
            </svg>
            <Input v-model="confirmPassword" placeholder="Confirmar contraseña" type="password" required class="bg-transparent border-none outline-none text-[#d3d3d3] w-full p-0 h-auto focus-visible:ring-0 focus-visible:ring-offset-0 shadow-none text-sm md:text-base" />
          </div>

          <button 
            type="submit" 
            :disabled="isLoading" 
            class="w-full bg-cyan-600 hover:bg-cyan-500 hover:shadow-[0_0_15px_rgba(6,182,212,0.4)] text-white border-none font-bold rounded-xl py-4 transition-all duration-300 mt-4 text-sm md:text-base cursor-pointer"
          >
            {{ isLoading ? 'Registrando...' : 'Registrarse' }}
          </button>
        </form>
      </CardContent>

      <CardFooter class="flex justify-center pt-2 pb-0">
        <button type="button" class="text-[#94a3b8] hover:text-cyan-500 text-sm md:text-base transition-colors cursor-pointer" @click="router.push('/login')">
          ¿Ya tienes cuenta? Inicia sesión
        </button>
      </CardFooter>
    </Card>
  </div>
</template>