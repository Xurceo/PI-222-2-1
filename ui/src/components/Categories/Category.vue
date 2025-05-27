<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { ICategory } from "../../types/Category.ts"
import type { ILot } from "../../types/Lot.ts";
import { fetchCategoryById } from "../../api/category_api.ts";

const props = defineProps<{
  id: string
}>()

const lots = ref<ILot[]>([]);
const category = ref<ICategory>();

onMounted(async () => {
  category.value = await fetchCategoryById(props.id);
  console.log(lots.value);
});
</script>

<template>
  <router-link
      :to="{ name: 'AddLotToCategory' }"
      class="flex justify-center border-2 rounded-lg border-gray-600 h-12 w-48 text-xl text-black m-10 bg-amber-100 hover:bg-amber-200 duration-300 cursor-pointer"
  >
    <div class="flex items-center"> <!-- Add items-center here -->
      <span class="m-2">Add Lot</span>
      <i class="pi pi-plus-circle"></i>
    </div>
  </router-link>
  <div class="text-black" v-if="lots">
    <h2>{{ category?.name }}</h2>
    <ul>
      <li v-for="lot in lots" :key="lot.id">
        <strong>{{ lot.title }}</strong>
        <br />
        <span>{{ lot.description }}</span>
      </li>
    </ul>
  </div>
</template>