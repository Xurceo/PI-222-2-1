<script setup lang="ts">
import type { ILot } from "../../types/Lot.ts";
import { ref, onMounted } from "vue";
import { getLotById } from "../../api/lot_api.ts";
import { useRouter } from "vue-router";

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
    class="lot-container mx-auto max-w-md w-full p-6 bg-white shadow-lg rounded-xl text-black"
  >
    <h2 class="text-2xl font-semibold mb-6 text-center">Lot Details</h2>
    <div v-if="lot">
      <p><strong>Title:</strong> {{ lot.title }}</p>
      <p><strong>Description:</strong> {{ lot.description }}</p>
      <p><strong>Starting Price:</strong> {{ lot.startingPrice }} $</p>
      <p><strong>Current Price:</strong> {{ lot.bids.sort()[0].amount }} $</p>
      <p><strong>Status:</strong> {{ lot.isEnded }}</p>
      <p>
        <strong>Created At:</strong>
        {{ new Date(lot.createdAt).toLocaleString() }}
      </p>
      <p>
        <strong>Updated At:</strong>
        {{ new Date(lot.updatedAt).toLocaleString() }}
      </p>
    </div>
    <div v-else>
      <p>Lot not found.</p>
    </div>
  </div>
</template>

<style scoped></style>
