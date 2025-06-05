<script setup lang="ts">
import { ref, onMounted } from "vue";
import { getLots } from "../../api/lot_api.ts";
import type { ILot } from "../../models/types/Lot.ts";

const lots = ref<ILot[]>([]);

onMounted(async () => {
  lots.value = await getLots();
});
</script>

<template>
  <div class="m-10 justify-center text-black" v-if="lots">
    <h1 class="m-10">Lots</h1>
    <ul class="flex flex-col items-start">
      <li
        class="relative before:content-['▸']"
        v-for="lot in lots"
        :key="lot.id!"
      >
        <router-link :to="{ name: 'Lot', params: { lotId: lot.id } }">
          {{ lot.title }}
        </router-link>
      </li>
    </ul>
  </div>
</template>
