<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getUserById } from "../../api/user_api.ts";

const user = ref<IUser>();

const props = defineProps<{
  id: string;
}>();

onMounted(async () => {
  const userId = props.id;
  if (userId) {
    try {
      user.value = await getUserById(userId);
      return { user };
    } catch (error) {
      route.push({ name: "NotFound" });
    }
  }
});
</script>

<template>
  <div
    class="profile-container mx-auto max-w-md w-full p-6 bg-white shadow-lg rounded-xl text-black"
  >
    <h2 class="text-2xl font-semibold mb-6 text-center">User Profile</h2>
    <div v-if="user">
      <p><strong>Username:</strong> {{ user.username }}</p>
      <div v-if="user.lots && user.lots.length > 0">
        <h3 class="mt-4">User's Lots</h3>
        <ul class="flex flex-col items-start">
          <li v-for="lot in user.lots" :key="lot.id">
            <router-link
              :to="{ name: 'Lot', params: { id: lot.id } }"
              class="text-blue-600 hover:underline"
            >
              {{ lot.title }}
            </router-link>
            <br />
            <span>{{ lot.description }}</span>
          </li>
        </ul>
      </div>
    </div>
    <div v-else>
      <p>User not found.</p>
    </div>
  </div>
</template>
