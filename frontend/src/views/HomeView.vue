<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import api from '@/api/axios' // Instancia centralizada con interceptor Anti-CSRF y cookies HttpOnly
import { Button } from '@/components/ui/button'

const router = useRouter()
const usuarioSesion = ref(null)
const isMenuOpen = ref(false) 

onMounted(() => {
  const sesionGuardada = localStorage.getItem('pegasus_user')
  if (sesionGuardada) {
    usuarioSesion.value = JSON.parse(sesionGuardada)
  }
})

const irALogin = () => router.push('/login')
const irARegistro = () => router.push('/registro')
const irAInventario = () => router.push('/vehiculos')

const cerrarSesion = async () => {
  try {
    // Llama al endpoint de cierre de sesión para invalidar la cookie HttpOnly en .NET
    await api.post('/auth/logout')
  } catch (error) {
    console.error('Error al cerrar sesión:', error)
  } finally {
    localStorage.removeItem('pegasus_user')
    usuarioSesion.value = null
    isMenuOpen.value = false
    router.push('/')
  }
}
</script>

<template>
  <div class="relative min-h-screen text-slate-100 font-sans overflow-hidden bg-slate-950">
    
    <!-- BARRA DE NAVEGACIÓN RESPONSIVA -->
    <nav class="absolute top-0 left-0 w-full flex items-center justify-between px-4 md:px-8 py-4 z-50">
      
      <!-- Logo -->
      <div class="flex items-center shrink-0">
        <img src="@/assets/logo.png" alt="Pegasus Logo" class="h-10 md:h-14 w-auto object-contain invert mix-blend-screen" />
      </div>
      
      <div class="flex items-center gap-2 md:gap-4">
        
        <template v-if="!usuarioSesion">
          <Button @click="irARegistro" variant="outline" class="bg-transparent border-slate-700 text-white hover:bg-slate-800 hover:text-white font-bold px-3 py-2 md:px-6 text-xs md:text-sm h-9 md:h-10">
            Registro
          </Button>
          <Button @click="irALogin" class="bg-cyan-600 hover:bg-cyan-500 text-white font-bold px-3 py-2 md:px-6 text-xs md:text-sm h-9 md:h-10">
            Iniciar Sesión
          </Button>
        </template>

        <template v-else>
          <div class="relative flex items-center gap-2 md:gap-4">
            <div class="flex flex-col text-right mr-1">
              <span class="font-bold text-white text-xs md:text-sm">{{ usuarioSesion.nombre }}</span>
              <span class="hidden sm:block text-[10px] md:text-xs text-cyan-400 font-semibold capitalize">{{ usuarioSesion.rol }}</span>
            </div>

            <button @click="isMenuOpen = !isMenuOpen" class="text-slate-300 hover:text-white transition-colors focus:outline-none p-1 bg-slate-800/50 rounded-md">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <line x1="3" y1="12" x2="21" y2="12"></line>
                <line x1="3" y1="6" x2="21" y2="6"></line>
                <line x1="3" y1="18" x2="21" y2="18"></line>
              </svg>
            </button>

            <div v-if="isMenuOpen" class="absolute top-12 right-0 w-52 bg-slate-900 border border-slate-700 rounded-md shadow-xl overflow-hidden z-50">
              <div class="flex flex-col">
                <button v-if="['vendedor', 'administrador'].includes(usuarioSesion.rol?.toLowerCase())" @click="irAInventario" class="px-4 py-3 text-left text-sm text-white hover:bg-slate-800 font-semibold border-b border-slate-800 transition-colors flex items-center gap-2">
                  ⚙️ Panel Admin
                </button>
                <button class="px-4 py-3 text-left text-sm text-white hover:bg-slate-800 font-semibold border-b border-slate-800 transition-colors">
                  👤 Mi Perfil
                </button>
                <button @click="cerrarSesion" class="px-4 py-3 text-left text-sm text-red-400 hover:bg-red-500/10 hover:text-red-300 font-semibold flex items-center gap-2 transition-colors">
                  Cerrar Sesión
                </button>
              </div>
            </div>
          </div>
        </template>
      </div>
    </nav>

    <!-- HÉROE -->
    <section class="relative min-h-screen flex items-center">
      <div class="container"></div>
      <div class="absolute inset-0 bg-gradient-to-r from-slate-950 via-slate-950/80 to-transparent z-0"></div>

      <!-- Ajuste principal responsivo: Anchos dinámicos (max-w-xl a max-w-6xl) y alineación dinámica (text-center a text-left) -->
      <div class="relative z-10 p-6 md:p-20 max-w-xl md:max-w-4xl lg:max-w-6xl mt-16 md:mt-0 mx-auto w-full text-center md:text-left">
        
        <h1 class="text-5xl sm:text-6xl md:text-8xl lg:text-9xl font-extrabold text-white mb-4 md:mb-6 leading-tight tracking-tight mt-10 md:mt-0">
          Excelencia <br class="hidden md:block"><span class="text-cyan-400">Multimarca</span>
        </h1>
        
        <p class="text-base sm:text-lg md:text-xl lg:text-2xl text-slate-300 mb-8 md:mb-10 max-w-2xl mx-auto md:mx-0 leading-relaxed">
          Descubre la mayor selección de modelos nuevos y usados de las mejores marcas del mundo. Tu próximo vehículo premium te espera.
        </p>
        
        <div class="flex flex-col sm:flex-row gap-4 md:gap-6 justify-center md:justify-start">
          <Button 
            @click="router.push('/registro')" 
            class="w-full sm:w-auto bg-transparent border border-slate-700 hover:border-cyan-500 hover:text-cyan-400 text-slate-200 py-6 md:px-12 font-bold tracking-widest text-xs md:text-sm uppercase transition-all duration-300 rounded-xl"
          >
            EXPLORAR CATÁLOGO
          </Button>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped>
.container {
  position: absolute;
  inset: 0;
}

.container::before {
  content: "";
  position: absolute;
  inset: -145%;
  rotate: -45deg;
  background: #000000;
  background-image: radial-gradient(4px 100px at 0px 235px, rgb(255, 140, 17), #0000),
    radial-gradient(4px 100px at 300px 235px, rgb(255, 119, 0), #884e2800),
    radial-gradient(1.5px 1.5px at 150px 117.5px, rgb(255, 144, 9) 100%, #0000 150%),
    radial-gradient(4px 100px at 0px 252px, rgb(156, 14, 137), #0000),
    radial-gradient(4px 100px at 300px 252px, rgb(23, 41, 206), #0000);
  background-size: 300px 235px, 300px 235px, 300px 235px, 300px 252px, 300px 252px;
  animation: hi 150s linear infinite;
}

@keyframes hi {
  0% { background-position: 0px 220px, 3px 220px, 151.5px 337.5px, 25px 24px, 28px 24px; }
  to { background-position: 0px 6800px, 3px 6800px, 151.5px 6917.5px, 25px 13632px, 28px 13632px; }
}
</style>