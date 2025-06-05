<template>
  <div class="">
    <h1 class="text-black m-20">Create Category</h1>
    <div class="flex flex-row justify-center form m-10">
      <input
        class="bg-white hover:bg-neutral-200 duration-300 text-black h-12 w-96 p-2 m-1 border-gray-600 border-2 rounded-lg"
        v-model="name"
        type="text"
        placeholder="EnterName"
      />
      <select
        class="bg-white hover:bg-neutral-200 duration-300 text-black mt-2 h-12 p-2 border-gray-600 border-2 rounded-lg select"
        v-model="parent"
      >
        <option :value="null">No parent</option>
        <option v-for="cat in categories" :key="cat.id!" :value="cat.id">
          {{ cat.name }}
        </option>
      </select>
      <button
        class="border-2 rounded-lg border-gray-600 h-12 w-48 text-xl text-black m-2 p-2 cursor-pointer bg-amber-100 hover:bg-amber-200 duration-300"
        @click="submit"
      >
        Submit
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { ICategory } from "../../models/types/Category.ts";
import { getCategories, addCategory } from "../../api/category_api.ts";
import router from "../../router.ts";

const name = ref<string>("");
const parent = ref<string | null>(null);
const categories = ref<ICategory[]>([]);

onMounted(async () => {
  categories.value = await getCategories();
});

const submit = async () => {
  if (!name.value.trim()) {
    alert("Name is required");
    return;
  }

  const category = {
    name: name.value,
    parentId: parent.value,
  } as ICategory;

  const categoryId = await addCategory(category);
  alert("Category Created Successfully!");
  router.push({ name: "Category", params: { categoryId: categoryId } });
};
</script>
