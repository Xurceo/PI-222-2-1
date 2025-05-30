<script setup lang="ts">
import { ref } from "vue";
import { login } from "../../api/user_api.ts";
import router from "../../router.ts";

const username = ref("");
const password = ref("");
const error = ref<string | null>(null);
const isLoading = ref(false);

const handleLogin = async () => {
  error.value = null;
  isLoading.value = true;

  try {
    await login(username.value, password.value);
    router.back();
    window.location.reload();
  } catch (err: any) {
    error.value = err.response?.data?.message || "Login failed";
  } finally {
    isLoading.value = false;
  }
};
</script>

<template>
  <form
    @submit.prevent="handleLogin"
    class="flex flex-col login-form mx-auto max-w-md w-full p-6 bg-white shadow-lg rounded-xl text-black"
  >
    <h2 class="text-2xl font-semibold mb-6 text-center">Login</h2>

    <!-- Username Field -->
    <div class="mb-4">
      <label for="username" class="block text-sm font-medium mb-1"
        >Username</label
      >
      <input
        id="username"
        v-model="username"
        type="text"
        placeholder="Enter username"
        required
        class="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring focus:ring-amber-300"
      />
    </div>

    <!-- Password Field -->
    <div class="mb-6">
      <label for="password" class="block text-sm font-medium mb-1"
        >Password</label
      >
      <input
        id="password"
        v-model="password"
        type="password"
        placeholder="Enter password"
        required
        class="w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring focus:ring-amber-300"
      />
    </div>

    <!-- Submit Button -->
    <button type="submit" :disabled="isLoading" class="w-full">
      {{ isLoading ? "Logging in..." : "Login" }}
    </button>

    <!-- Error Message -->
    <p v-if="error" class="mt-4 text-sm text-red-600 text-center">
      {{ error }}
    </p>
  </form>
</template>

<style scoped>
input {
  padding: 0.5rem;
  font-size: 1rem;
  width: 100%;
}

button {
  padding: 0.5rem;
  font-size: 1rem;
  cursor: pointer;
}

.error {
  color: red;
}
</style>
