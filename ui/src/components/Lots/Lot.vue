<script setup lang="ts">
import type { ILot } from "../../models/types/Lot.ts";
import { ref, onMounted } from "vue";
import { getLotById } from "../../api/lot_api.ts";
import { useRouter } from "vue-router";
import { statusMap } from "../../models/maps/statusMap.ts";

const lot = ref<ILot>();

const props = defineProps<{
  id: string;
}>();

onMounted(async () => {
  const lotId = props.id;
  if (lotId) {
    try {
      lot.value = await getLotById(lotId);
    } catch (error) {
      useRouter().push({ name: "NotFound" });
    }
  }
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
      <p><strong>Current Price:</strong> {{ lot.bids.sort()[0].amount }} $</p>
      <p><strong>Status:</strong> {{ statusMap[lot.status] }}</p>
      <p><strong>Category:</strong> {{ lot.category.name }}</p>
      <p v-if="lot.winner">
        <strong>Winner:</strong> {{ lot.winner.username }}
      </p>
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
          <tr v-for="bid in lot.bids" :key="bid.id" class="border-t">
            <td class="px-4 py-2">
              <router-link
                :to="{ name: 'Profile', params: { id: bid.user.id } }"
                class="text-blue-600 hover:underline"
              >
                {{ bid.user.username }}
              </router-link>
            </td>
            <td class="px-4 py-2">{{ bid.amount }} $</td>
            <td class="px-4 py-2">
              {{ new Date(bid.time).toLocaleString() }}
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
