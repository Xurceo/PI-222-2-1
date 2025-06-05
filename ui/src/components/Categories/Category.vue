<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { ICategory } from "../../models/types/Category.ts";
import type { ILot } from "../../models/types/Lot.ts";
import { getCategoryById, getCategoryLots } from "../../api/category_api.ts";

const props = defineProps<{
  categoryId: string;
}>();

const category = ref<ICategory>();
const categoryLots = ref<ILot[]>([]);

onMounted(async () => {
  category.value = await getCategoryById(props.categoryId);
  categoryLots.value = await getCategoryLots(props.categoryId);
});
</script>

<template>
  <div class="m-10 justify-center text-black" v-if="category">
    <h1 class="m-10">{{ category.name }}</h1>
    <ul class="flex flex-col items-start">
      <li
        class="relative before:content-['▸']"
        v-for="lot in categoryLots"
        :key="lot.id!"
      >
        <router-link :to="{ name: 'Lot', params: { lotId: lot.id } }">
          {{ lot.title }}
        </router-link>
      </li>
    </ul>
  </div>
</template>
