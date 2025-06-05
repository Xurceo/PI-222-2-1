<script setup lang="ts">
import AppHeader from "./components/Shared/AppHeader.vue";
import AppFooter from "./components/Shared/AppFooter.vue";
import { useAuth } from "./composables/useAuth";

const { loadUser } = useAuth();
loadUser();
</script>

<template>
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <div class="flex flex-col min-h-screen min-w-screen w-full">
    <AppHeader />
    <main class="flex-grow w-full">
      <router-view />
    </main>
    <AppFooter />
  </div>
</template>
