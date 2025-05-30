<template>
  <header
    class="flex items-center justify-between p-4 mb-4 shadow-md bg-neutral-200"
  >
    <!-- Left: Home -->
    <router-link
      class="text-4xl font-bold text-black hover:text-neutral-600 transition duration-200"
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
      <span class="mx-2 h-8 border-l border-blue-600"></span>
      <div class="nav-item cursor-pointer" @click="handleLogout">Logout</div>
    </div>
    <div v-else class="text-2xl text-black mr-4">
      <div>
        <router-link class="nav-item cursor-pointer" :to="{ name: 'Login' }">
          Login
        </router-link>
      </div>
    </div>
  </header>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getCurrentUser, logout } from "../../api/user_api.ts";
import type { IUser } from "../../types/User.ts";

const currentUser = ref<IUser>();

onMounted(async () => {
  try {
    currentUser.value = await getCurrentUser();
    currentUser.value = currentUser.value.user;
  } catch (error) {
    console.error("Failed to fetch current user:", error);
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
