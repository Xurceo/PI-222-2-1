<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";
import type { ICategory } from "../../models/types/Category.ts";
import { getCategories } from "../../api/category_api.ts";

export default defineComponent({
  setup() {
    const categories = ref<ICategory[]>([]);

    onMounted(async () => {
      categories.value = await getCategories();
      console.log(categories.value);
    });

    return { categories };
  },
});
</script>

<template>
  <router-link
    :to="{ name: 'AddCategory' }"
    class="flex justify-center border-2 rounded-lg border-gray-600 h-12 w-48 text-xl text-black m-10 bg-amber-100 hover:bg-amber-200 duration-300 cursor-pointer"
  >
    <div class="flex items-center">
      <!-- Add items-center here -->
      <span class="m-2">Add Category</span>
      <i class="pi pi-plus-circle"></i>
    </div>
  </router-link>
  <div class="m-10 justify-center text-black" v-if="categories">
    <h1 class="m-10">Categories</h1>
    <ul class="flex flex-col items-start">
      <li
        class="relative before:content-['▸']"
        v-for="cat in categories"
        :key="cat.id"
      >
        <router-link :to="{ name: 'Category', params: { id: cat.id } }">
          {{ cat.name }}
        </router-link>
      </li>
    </ul>
  </div>
</template>
