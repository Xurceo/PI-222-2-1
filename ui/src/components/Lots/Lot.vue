<script setup lang="ts">
import type { ILot } from "../../models/types/Lot.ts";
import { ref, onMounted, onUnmounted, computed } from "vue";
import { getLotById } from "../../api/lot_api.ts";
import { statusMap } from "../../models/maps/statusMap.ts";
import { IUser } from "../../models/types/User.ts";
import { getAllUsers } from "../../api/user_api.ts";

const lot = ref<ILot>();
const bids = ref<ILot["bids"]>([]);
const users = ref<IUser[]>([]);
const now = ref(new Date());
let intervalId: number | undefined = undefined;

const props = defineProps<{
  id: string;
}>();

onMounted(async () => {
  const lotId = props.id;
  intervalId = setInterval(() => {
    now.value = new Date();
  }, 1000);
  if (lotId) {
    try {
      lot.value = await getLotById(lotId);
      users.value = await getAllUsers();
      if (lot.value) {
        bids.value = lot.value.bids.sort((a, b) => b.amount - a.amount);
      }
      bids.value.sort((b) => b.amount);

      if (bids.value) {
        bids.value = bids.value.map((bid) => ({
          ...bid,
          user: users.value.find((u) => u.id === bid.userId) || null,
        }));
      }
    } catch (error) {
      console.error("Error fetching lot:", error);
    }
  }
});

onUnmounted(() => {
  clearInterval(intervalId);
});

const elapsed = computed<string | number>(() => {
  const nowTime = now.value;
  if (!lot.value || !lot.value.startTime) return 0;
  const endTime = new Date(lot.value.endTime);

  const seconds = Math.floor((endTime.getTime() - nowTime.getTime()) / 1000);
  const minutes = Math.floor(seconds / 60);
  const hours = Math.floor(minutes / 60);
  const days = Math.floor(hours / 24);
  const months = Math.floor(days / 30);
  const years = Math.floor(months / 12);

  let result = `${hours % 24}:${minutes % 60}:${seconds % 60}`;
  if (years > 0) result = `${years} years, ${result}`;
  if (months > 0) result = `${months} months, ${result}`;
  if (days > 0) result = `${days} days, ${result}`;
  result = `Time left: ${result}`;

  return result;
});
</script>

<template>
  <div
    class="mx-auto max-w-md w-full p-6 bg-white shadow-lg rounded-xl text-black"
  >
    <h2 class="text-2xl font-semibold mb-6 text-center">Lot Details</h2>
    <div v-if="lot">
      <p><strong>Title:</strong> {{ lot.title }}</p>
      <p><strong>Owner:</strong> {{ lot.owner.username }}</p>
      <p><strong>Description:</strong> {{ lot.description }}</p>
      <p><strong>Starting Price:</strong> {{ lot.startPrice }} $</p>
      <p>
        <strong>Current Price:</strong>
        {{ bids.length > 0 ? bids[0].amount : lot.startPrice }} $
      </p>
      <p><strong>Status:</strong> {{ statusMap[lot.status] }}</p>
      <p><strong>Category:</strong> {{ lot.category.name }}</p>
      <p v-if="lot.winner">
        <strong>Winner:</strong> {{ lot.winner.username }}
      </p>

      <p v-if="lot.status == 2">{{ elapsed }}</p>

      <router-link
        v-if="lot.status === 2"
        :to="{ name: 'PlaceBid', params: { lotId: lot.id } }"
        class="flex flex-col button min-w-full pt-1.5 mt-4"
        >Place Bid</router-link
      >

      <table
        v-if="lot.bids && lot.bids.length > 0"
        class="min-w-full mt-4 border rounded"
      >
        <thead>
          <tr class="bg-gray-100">
            <th class="px-4 py-2 text-left">Username</th>
            <th class="px-4 py-2 text-left">Amount</th>
            <th class="px-4 py-2 text-left">Time</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="bid in bids" :key="bid.id" class="border-t">
            <td class="px-4 py-2">
              <router-link
                :to="{ name: 'Profile', params: { id: bid.user?.id } }"
                class="text-blue-600 hover:underline"
              >
                {{ bid.user.username }}
              </router-link>
            </td>
            <td class="px-4 py-2">{{ bid.amount }} $</td>
            <td class="px-4 py-2">
              {{ bid.time.toLocaleString() }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div v-else>
      <p>Lot not found.</p>
    </div>
  </div>
</template>

<style scoped></style>
