<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { flattenCategories } from '../../utils/flatten.ts'
import axios from 'axios';
import type { ICategory } from "../../types/Category.ts"
import type { ILot } from "../../types/Lot.ts";

const api = import.meta.env.VITE_API_URL;

export default defineComponent({
  setup() {
    const lots = ref<ILot[]>([]);

    onMounted(async () => {
      try{
        const res = await axios.get<ILot[]>(`${api}/lots`);
        lots.value = res.data;
      } catch (error) {
        if (axios.isAxiosError(error)) {
          console.error('Axios error:', error.response?.data || error.message);
        } else {
          console.error('Unexpected error:', error);
        }
      }
      console.log(lots.value);
    });

    return { lots };
  },
});

</script>

<template>
  <div class="text-black" v-if="lots">
    <h1 class="m-10">Lots</h1>
    <ul>
      <li v-for="lot in lots" :key="lot.id">
        <strong>{{ lot.title }}</strong>
        <br />
        <span>{{ lot.description }}</span>
      </li>
    </ul>
  </div>
</template>