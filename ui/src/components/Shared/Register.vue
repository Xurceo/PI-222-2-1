<script setup lang="ts">
import { ref } from "vue";
import { registerUser } from "../../api/user_api.ts";
import router from "../../router.ts";
import type { IRegisterUser } from "../../models/types/RegisterUser.ts";

const username = ref("");
const password = ref("");
const error = ref<string | null>(null);
const isLoading = ref(false);

const handleRegister = async () => {
  error.value = null;
  isLoading.value = true;

  try {
    if (!username.value || !password.value) {
      throw new Error("Username and password are required");
    }
    const userData: IRegisterUser = {
      username: username.value,
      password: password.value,
    };
    const user = await registerUser(userData);
    window.location.reload();
    router.push({ name: "Profile", params: { id: user.id } });
  } catch (err: any) {
    error.value = err.response?.data?.message || "Register failed";
  } finally {
    isLoading.value = false;
  }
};
</script>

<template>
  <form
    @submit.prevent="handleRegister"
    class="flex flex-col login-form mx-auto max-w-md w-full p-6 bg-white shadow-2xl shadow-teal-900 rounded-xl text-black"
  >
    <h2 class="text-2xl font-semibold mb-6 text-center">Register</h2>

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
      {{ isLoading ? "Registering ..." : "Register" }}
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
