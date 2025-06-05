<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getUserBids, getUserById, getUserLots } from "../../api/user_api.ts";
import type { IUser } from "../../models/types/User.ts";
import router from "../../router.ts";
import { capitalize } from "../../lib/auction.ts";
import { useAuth } from "../../composables/useAuth.ts";
import type { ILot } from "../../models/types/Lot.ts";
import type { IBid } from "../../models/types/Bid.ts";

const user = ref<IUser>();
const userLots = ref<ILot[]>([]);
const userBids = ref<IBid[]>([]);

const { currentUser } = useAuth();

const props = defineProps<{
  userId: string;
}>();

onMounted(async () => {
  user.value = await getUserById(props.userId);
  userLots.value = await getUserLots(props.userId);
  userBids.value = await getUserBids(props.userId);
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
          :to="{ name: 'AddLot', params: { userId: user.id } }"
        >
          Create Lot
        </router-link>
      </div>
      <div v-if="userLots && userLots.length > 0">
        <h3 class="mt-4">User's Lots</h3>
        <ul class="grid grid-cols-3 gap-4 items-start w-full">
          <router-link
            :to="{ name: 'Lot', params: { lotId: lot.id } }"
            v-for="lot in userLots"
            :key="lot.id!"
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
