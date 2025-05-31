<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";
import { getLots } from "../../api/lot_api.ts";
import axios from "axios";
import type { ICategory } from "../../models/types/Category.ts";
import type { ILot } from "../../models/types/Lot.ts";

const api = import.meta.env.VITE_API_URL;

export default defineComponent({
  setup() {
    const lots = ref<ILot[]>([]);

    onMounted(async () => {
      try {
        lots.value = await getLots();
      } catch (error) {
        if (axios.isAxiosError(error)) {
          console.error("Axios error:", error.response?.data || error.message);
        } else {
          console.error("Unexpected error:", error);
        }
      }
      console.log(lots.value);
    });

    return { lots };
  },
});
</script>

<template>
  <div class="m-10 justify-center text-black" v-if="lots">
    <h1 class="m-10">Lots</h1>
    <ul class="flex flex-col items-start">
      <li
        class="relative before:content-['▸']"
        v-for="lot in lots"
        :key="lot.id"
      >
        <router-link :to="{ name: 'Lot', params: { id: lot.id } }">
          {{ lot.title }}
        </router-link>
      </li>
    </ul>
  </div>
</template>
