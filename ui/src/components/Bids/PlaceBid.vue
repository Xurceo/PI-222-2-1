<script setup lang="ts">
import { ref, onMounted } from "vue";
import { placeBid } from "../../api/bid_api";
import { getCurrentUser } from "../../api/user_api";
import { getLotById } from "../../api/lot_api";
import type { IBid } from "../../models/types/Bid";
import type { IUser } from "../../models/types/User";
import type { ILot } from "../../models/types/Lot";
import router from "../../router";

const bidAmount = ref<number | null>(null);
const errorMessage = ref<string | null>(null);
const currentUser = ref<IUser | null>(null);
const lot = ref<ILot>();

const props = defineProps<{
  lotId: string;
}>();

onMounted(async () => {
  try {
    currentUser.value = await getCurrentUser();
  } catch (error) {
    currentUser.value = null; // Explicitly set to null if not authed
    console.error("Failed to fetch current user:", error);
  }
});

const placeBidHandler = async () => {
  if (!currentUser.value) {
    errorMessage.value = "You must be logged in to place a bid.";
    return;
  }
  try {
    lot.value = await getLotById(props.lotId);
  } catch (error) {
    console.error("Failed to fetch lot:", error);
  }

  if (lot.value === undefined || bidAmount.value === null) {
    errorMessage.value = "Please enter both Lot ID and Bid Amount.";
    return;
  }

  const highestBid =
    lot.value.bids.length > 0
      ? lot.value.bids.sort((a, b) => b.amount - a.amount)[0].amount
      : 0;

  if (bidAmount.value <= 0) {
    errorMessage.value = "Bid amount must be greater than zero.";
    return;
  } else if (lot.value!.bids.length > 0 && bidAmount.value <= highestBid) {
    errorMessage.value = `Bid amount must be greater than the current highest bid (${highestBid}$).`;
    return;
  } else if (lot.value!.status !== 2) {
    errorMessage.value = "You can only place bids on confirmed lots.";
    return;
  }

  try {
    await placeBid(lot.value!.id, bidAmount.value);
    errorMessage.value = null;
    alert("Bid placed successfully!");
    setTimeout(() => {
      router.push({ name: "Lot", params: { id: lot.value!.id } });
      window.location.reload();
    }, 1000); // Reload the page after 1 second to reflect the new bid
  } catch (error) {
    errorMessage.value = "Failed to place bid. Please try again.";
    console.error("Error placing bid:", error);
  }
};
</script>
<template>
  <div
    class="place-bid-container mx-auto max-w-md w-full p-6 bg-white shadow-lg rounded-xl text-black"
  >
    <h2 class="text-2xl font-semibold mb-6 text-center">Place a Bid</h2>
    <div v-if="currentUser">
      <p><strong>Current User:</strong> {{ currentUser.username }}</p>
    </div>
    <div v-else>
      <p>Please log in to place a bid.</p>
    </div>
    <form @submit.prevent="placeBidHandler" class="space-y-4">
      <div>
        <label for="bidAmount" class="block text-sm font-medium text-gray-700"
          >Bid Amount ($)</label
        >
        <input
          v-model.number="bidAmount"
          type="number"
          id="bidAmount"
          class="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:border-blue-500 focus:ring-blue-500"
          required
        />
      </div>
      <button type="submit" class="button min-w-full">Place Bid</button>
      <div v-if="errorMessage" class="text-red-500 mt-2">
        {{ errorMessage }}
      </div>
    </form>
  </div>
</template>
