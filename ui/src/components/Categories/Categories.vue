<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";
import type { ICategory } from "../../models/types/Category.ts";
import { getCategories } from "../../api/category_api.ts";

export default defineComponent({
  setup() {
    const categories = ref<ICategory[]>([]);

    onMounted(async () => {
      categories.value = await getCategories();
    });

    return { categories };
  },
});
</script>

<template>
  <div class="m-10 justify-center text-black" v-if="categories">
    <h1 class="m-10">Categories</h1>
    <ul class="flex flex-col items-start">
      <li
        class="relative before:content-['▸']"
        v-for="cat in categories"
        :key="cat.id!"
      >
        <router-link :to="{ name: 'Category', params: { categoryId: cat.id } }">
          {{ cat.name }}
        </router-link>
      </li>
    </ul>
  </div>
</template>
