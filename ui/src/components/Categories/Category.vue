<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { ICategory } from "../../models/types/Category.ts";
import type { ILot } from "../../models/types/Lot.ts";
import { getCategoryById } from "../../api/category_api.ts";

const props = defineProps<{
  id: string;
}>();

const lots = ref<ILot[]>([]);
const category = ref<ICategory>();

onMounted(async () => {
  category.value = await getCategoryById(props.id);
  console.log(lots.value);
});
</script>

<template>
  <router-link
    :to="{ name: 'AddLotToCategory' }"
    class="flex justify-center border-2 rounded-lg border-gray-600 h-12 w-48 text-xl text-black m-10 bg-amber-100 hover:bg-amber-200 duration-300 cursor-pointer"
  >
    <div class="flex items-center">
      <!-- Add items-center here -->
      <span class="m-2">Add Lot</span>
      <i class="pi pi-plus-circle"></i>
    </div>
  </router-link>
  <div class="m-10 justify-center text-black" v-if="category">
    <h1 class="m-10">{{ category.name }}</h1>
    <ul class="flex flex-col items-start">
      <li
        class="relative before:content-['▸']"
        v-for="lot in category.lots"
        :key="lot.id"
      >
        <router-link :to="{ name: 'Lot', params: { id: lot.id } }">
          {{ lot.title }}
        </router-link>
      </li>
    </ul>
  </div>
</template>
