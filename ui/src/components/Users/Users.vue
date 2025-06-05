<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getAllUsers } from "../../api/user_api.ts";
import type { IUser } from "../../models/types/User.ts";

const users = ref<IUser[]>([]);
onMounted(async () => {
  try {
    users.value = await getAllUsers();
  } catch (error) {
    console.error("Failed to fetch users:", error);
  }
});
</script>

<template>
  <div class="users-container">
    <h1 class="text-black p-10 text-2xl mb-6">Users</h1>
    <ul class="user-list">
      <li v-for="user in users" :key="user.id" class="user-item">
        <router-link :to="{ name: 'Profile', params: { id: user.id } }">
          {{ user.username }}
        </router-link>
      </li>
    </ul>
  </div>
  <div v-if="users.length === 0" class="text-center text-gray-500">
    No users found.
  </div>
</template>

<style scoped></style>
