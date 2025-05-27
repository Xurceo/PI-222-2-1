<template>
  <header class=" text-start p-2 shadow-md bg-neutral-200">
    <router-link class="text-4xl text-shadow-md text-black border-b-neutral-200 cursor-pointer" :to="{ name: 'Home' }">Home</router-link>

    <div class="end-0 text-shadow-md text-2xl">
      <router-link class="nav-item m-10 cursor-pointer" :to="{ name: 'Lots' }">Lots</router-link>
      <router-link class="nav-item m-10 cursor-pointer" :to="{ name: 'Categories' }">Categories</router-link>
      <router-link class="nav-item m-10 cursor-pointer" :to="{ name: 'Users' }">Users</router-link>
    </div>
  </header>
</template>

<script setup lang="ts">
import router from '../../router.ts'
</script>