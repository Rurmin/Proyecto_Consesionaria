<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import api from '@/api/axios'

const router = useRouter()
const activeTab = ref('inicio')
const usuario = ref({ nombre: 'Administrador', email: '', rol: 'Administrador' })

onMounted(async () => {
  try {
    const response = await api.get('/auth/me')
    usuario.value = response.data
  } catch (e) {
    console.warn('No se pudo cargar la información del usuario:', e)
  }
})

const irA = (ruta) => router.push(ruta)

const cerrarSesion = async () => {
  try {
    await api.post('/auth/logout')
  } catch (error) {
    console.error('Error al cerrar sesión:', error)
  } finally {
    localStorage.removeItem('pegasus_user')
    router.push('/')
  }
}
</script>

<template>
  <!-- Contenedor principal: Fila en escritorio, Columna en móvil -->
  <div class="min-h-screen bg-slate-950 text-white flex flex-col md:flex-row">
    
    <!-- SIDEBAR (ESCRITORIO): Oculto en móvil, visible en md+ -->
    <aside class="hidden md:flex flex-col w-64 bg-slate-900 border-r border-slate-800 p-6 justify-between shrink-0">
      <div>
        <div class="flex items-center gap-3 mb-8">
          <div class="w-10 h-10 rounded-lg bg-cyan-500 flex items-center justify-center font-bold text-slate-950 text-xl">A</div>
          <span class="font-bold tracking-wider text-sm text-cyan-400 uppercase leading-tight">Concesionario<br>Móvil</span>
        </div>
        
        <nav class="flex flex-col gap-2">
          <button @click="activeTab = 'inicio'" :class="[activeTab === 'inicio' ? 'bg-cyan-900/30 text-cyan-400' : 'text-slate-400 hover:bg-slate-800', 'flex items-center gap-3 px-4 py-3 rounded-xl transition-colors font-semibold text-sm']">
            <span>Inicio</span>
          </button>
          <button @click="activeTab = 'consulta'" :class="[activeTab === 'consulta' ? 'bg-cyan-900/30 text-cyan-400' : 'text-slate-400 hover:bg-slate-800', 'flex items-center gap-3 px-4 py-3 rounded-xl transition-colors font-semibold text-sm']">
            <span>Consulta</span>
          </button>
          <button @click="activeTab = 'notificaciones'" :class="[activeTab === 'notificaciones' ? 'bg-cyan-900/30 text-cyan-400' : 'text-slate-400 hover:bg-slate-800', 'flex items-center gap-3 px-4 py-3 rounded-xl transition-colors font-semibold text-sm']">
            <span>Notificaciones</span>
          </button>
        </nav>
      </div>

      <div class="border-t border-slate-800 pt-6">
        <button @click="activeTab = 'perfil'" class="flex items-center gap-3 w-full mb-4">
          <div class="w-10 h-10 bg-cyan-600 rounded-full flex items-center justify-center font-bold text-white shadow-lg">
            {{ usuario.nombre?.charAt(0)?.toUpperCase() || 'A' }}
          </div>
          <div class="text-left flex-1 overflow-hidden">
            <p class="font-bold text-sm text-white truncate">{{ usuario.nombre }}</p>
            <p class="text-xs text-slate-400 truncate">{{ usuario.rol }}</p>
          </div>
        </button>
        <button @click="cerrarSesion" class="w-full py-2 bg-red-600/10 hover:bg-red-600/20 text-red-400 rounded-lg text-xs font-semibold transition">
          Cerrar Sesión
        </button>
      </div>
    </aside>

    <!-- ÁREA DE CONTENIDO PRINCIPAL -->
    <main class="flex-1 flex flex-col h-screen overflow-y-auto pb-24 md:pb-0 p-6 md:p-10">
      <header class="mb-8 md:hidden">
        <!-- Encabezado móvil original (se oculta en escritorio porque el sidebar ya lo tiene) -->
        <div class="flex items-center justify-between mb-3">
          <span class="text-[10px] px-2.5 py-1 bg-cyan-950/80 border border-cyan-700/50 rounded-full text-cyan-300 font-semibold uppercase">
            {{ usuario.rol }}
          </span>
        </div>
        <h1 class="text-2xl font-black tracking-tight">¡Hola, {{ usuario.nombre }}!</h1>
      </header>

      <header class="hidden md:block mb-10">
        <h1 class="text-3xl font-black tracking-tight">Panel de Control</h1>
        <p class="text-slate-400 text-sm font-semibold uppercase tracking-wider mt-1">Gestión General del Sistema</p>
      </header>

      <!-- Aquí va tu contenido dinámico (los v-if de activeTab) envueltos en un contenedor que no se estire demasiado en PC -->
      <div class="max-w-3xl w-full">
        <!-- Mantén tus templates de activeTab originales aquí -->
        <template v-if="activeTab === 'inicio'">
          <div class="bg-slate-900 border border-slate-800 rounded-2xl p-8 text-left shadow-lg">
            <h2 class="text-xl font-bold mb-2">Bienvenido al sistema</h2>
            <p class="text-slate-400 text-sm">Selecciona un módulo en el menú para comenzar a operar.</p>
          </div>
        </template>
        <!-- Resto de tabs... -->
      </div>
    </main>

    <!-- BOTTOM NAV (MÓVIL): Visible solo en pantallas pequeñas (hidden en md) -->
    <nav class="md:hidden fixed bottom-0 left-0 right-0 bg-slate-900/95 backdrop-blur-md border-t border-slate-800 py-2.5 px-4 flex justify-around items-center z-50">
      <!-- Mantén tus botones de navegación móvil originales aquí -->
    </nav>
  </div>
</template>