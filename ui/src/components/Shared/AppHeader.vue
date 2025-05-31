<template>
  <header
    class="flex items-center justify-between p-4 mb-4 shadow-md bg-neutral-200"
  >
    <!-- Left: Home -->
    <router-link
      class="text-4xl font-bold text-black text-shadow-lg text-shadow-fuchsia-700 hover:text-neutral-600 transition duration-200"
      :to="{ name: 'Home' }"
    >
      Auction
    </router-link>

    <!-- Center: Navigation Links -->
    <nav class="flex gap-8 text-2xl">
      <router-link class="nav-item cursor-pointer" :to="{ name: 'Lots' }">
        Lots
      </router-link>
      <router-link class="nav-item cursor-pointer" :to="{ name: 'Categories' }">
        Categories
      </router-link>
      <router-link class="nav-item cursor-pointer" :to="{ name: 'Users' }">
        Users
      </router-link>
    </nav>

    <!-- Right: Login -->
    <div v-if="currentUser" class="flex flex-row text-2xl text-black mr-4">
      <router-link
        class="nav-item cursor-pointer"
        :to="{ name: 'Profile', params: { id: currentUser.id } }"
      >
        Welcome, {{ currentUser.username }}
      </router-link>
      <span class="mx-2 h-10 border-gray-300 border"></span>
      <div class="nav-item cursor-pointer" @click="handleLogout">Logout</div>
    </div>
    <div v-else class="text-2xl text-black mr-4">
      <router-link class="nav-item cursor-pointer" :to="{ name: 'Login' }">
        Login
      </router-link>
      <span class="mx-2 h-10 border-gray-300 border"></span>
      <router-link class="nav-item cursor-pointer" :to="{ name: 'Register' }">
        Register
      </router-link>
    </div>
  </header>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getCurrentUser, logout } from "../../api/user_api.ts";
import type { IUser } from "../../models/types/User.ts";
import { cookieExists } from "../../lib/auction.ts";

const currentUser = ref<IUser | null>(null);

onMounted(async () => {
  try {
    if (cookieExists("jwt")) {
      return;
    }
    currentUser.value = await getCurrentUser();
  } catch (error) {
    currentUser.value = null; // Explicitly set to null if not authed
    return;
  }
});

const handleLogout = async () => {
  try {
    await logout(); // Assuming this function handles logout
    currentUser.value = null; // Clear the current user
    window.location.reload(); // Reload the page to reflect changes
  } catch (error) {
    console.error("Logout failed:", error);
  }
};
</script>
