<script setup lang="ts">
import { defineComponent, ref, onMounted, useAttrs } from "vue";
import type { ICategory } from "../../models/types/Category.ts";
import { getCategories } from "../../api/category_api.ts";
import { useAuth } from "../../composables/useAuth.ts";

const categories = ref<ICategory[]>([]);

const { currentUser } = useAuth();

onMounted(async () => {
  categories.value = await getCategories();
});
</script>

<template>
  <div
    v-if="
      currentUser &&
      (currentUser.role === `MANAGER` || currentUser.role === `ADMIN`)
    "
  >
    <router-link
      class="button pt-1.5 mt-4 w-56 ml-4"
      :to="{ name: 'AddCategory' }"
      >Add Category</router-link
    >
  </div>
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
