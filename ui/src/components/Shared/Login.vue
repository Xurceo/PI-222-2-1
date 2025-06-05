<script setup lang="ts">
import { ref } from "vue";
import { apiLogin } from "../../api/user_api.ts";
import router from "../../router.ts";
import { useAuth } from "../../composables/useAuth.ts";

const { currentUser } = useAuth();

const username = ref("");
const password = ref("");
const error = ref<string | null>(null);
const isLoading = ref(false);

const handleLogin = async () => {
  error.value = null;
  isLoading.value = true;

  try {
    const user = await apiLogin(username.value, password.value);
    currentUser.value = user;
    router.back();
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
    class="flex flex-col login-form mx-auto max-w-md w-full p-6 bg-white shadow-2xl shadow-teal-900 rounded-xl text-black"
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
        class="w-full"
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
        class="w-full"
      />
    </div>

    <!-- Submit Button -->
    <button type="submit" :disabled="isLoading" class="w-full pt-1.5">
      {{ isLoading ? "Logging in..." : "Login" }}
    </button>

    <!-- Error Message -->
    <p v-if="error" class="mt-4 text-sm text-red-600 text-center">
      {{ error }}
    </p>
  </form>
</template>

<style scoped>
.error {
  color: red;
}
</style>
