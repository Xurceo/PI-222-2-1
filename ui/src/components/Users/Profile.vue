<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getUserById } from "../../api/user_api.ts";
import type { IUser } from "../../models/types/User.ts";
import router from "../../router.ts";
import { capitalize } from "../../lib/auction.ts";
import { useAuth } from "../../composables/useAuth.ts";

const user = ref<IUser>();
const { currentUser } = useAuth();

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
      router.push({ name: "NotFound" });
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
      <p><strong>Role:</strong> {{ capitalize(user.role) }}</p>
      <div v-if="user.id == currentUser?.id">
        <router-link
          class="button mt-4"
          :to="{ name: 'AddLot', params: { id: user.id } }"
        >
          Create Lot
        </router-link>
      </div>
      <div v-if="user.lots && user.lots.length > 0">
        <h3 class="mt-4">User's Lots</h3>
        <ul class="grid grid-cols-3 gap-4 items-start w-full">
          <router-link
            :to="{ name: 'Lot', params: { id: lot.id } }"
            v-for="lot in user.lots"
            :key="lot.id"
            class="button pt-1.5"
          >
            {{ lot.title }}
          </router-link>
        </ul>
      </div>
    </div>
    <div v-else>
      <p>User not found.</p>
    </div>
  </div>
</template>
