<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { ICategory } from "../../models/types/Category.ts";
import { getCategories, getCategoryById } from "../../api/category_api.ts";
import { addLot } from "../../api/lot_api.ts";
import { useAuth } from "../../composables/useAuth.ts";
import router from "../../router.ts";
import type { ILot } from "../../models/types/Lot.ts";

const { currentUser } = useAuth();

const title = ref<string>("");
const endTime = ref<string>("");
const startPrice = ref<number>(0);
const description = ref<string>("");
const categoryId = ref<string>("");
const categories = ref<ICategory[]>([]);

const props = defineProps<{
  userId: string;
}>();

onMounted(async () => {
  if (!currentUser.value) {
    alert("You must be logged in to create a lot.");
    return;
  }
  categories.value = await getCategories();
});

const createLot = async () => {
  if (!title.value.trim() || !endTime.value.trim() || startPrice.value <= 0) {
    alert("All fields are required.");
    return;
  }

  const newLot = {
    title: title.value,
    endTime: new Date(endTime.value),
    startPrice: startPrice.value,
    description: description.value,
    categoryId: categoryId.value,
  } as ILot;
  try {
    await addLot(newLot);
    router.push({ name: "Lots" });
  } catch (error) {
    return;
  }
};
</script>

<template>
  <div
    class="add-lot-container mx-auto max-w-md w-full p-6 bg-white shadow-lg rounded-xl text-black"
  >
    <h2 class="text-2xl font-semibold mb-6 text-center">Add Lot</h2>
    <form @submit.prevent="createLot">
      <div class="mb-4">
        <label for="title" class="block text-sm font-medium mb-2">Title</label>
        <input
          v-model="title"
          type="text"
          id="title"
          class="w-full p-2 border border-gray-300 rounded"
          required
        />
      </div>
      <div class="mb-4">
        <label for="endTime" class="block text-sm font-medium mb-2"
          >End Time</label
        >
        <input
          v-model="endTime"
          type="datetime-local"
          id="endTime"
          class="w-full p-2 border border-gray-300 rounded"
          required
        />
      </div>
      <div class="mb-4">
        <label for="startPrice" class="block text-sm font-medium mb-2"
          >Start Price</label
        >
        <input
          v-model.number="startPrice"
          type="number"
          id="startPrice"
          class="w-full p-2 border border-gray-300 rounded"
          required
        />
      </div>
      <div class="mb-4">
        <label for="description" class="block text-sm font-medium mb-2"
          >Description</label
        >
        <textarea
          v-model="description"
          id="description"
          rows="3"
          class="w-full p-2 border border-gray-300 rounded"
        ></textarea>
      </div>
      <div class="mb-4">
        <label for="category" class="block text-sm font-medium mb-2"
          >Category</label
        >
        <select
          class="bg-white hover:bg-neutral-200 duration-300 text-black h-12 w-full p-2 border-gray-600 border-2 rounded-lg select"
          v-model="categoryId"
        >
          <option value="" disabled selected>Select Category</option>
          <option v-for="cat in categories" :key="cat.id!" :value="cat.id">
            {{ cat.name }}
          </option>
        </select>
      </div>
      <button type="submit" class="button w-full mt-4">Create Lot</button>
    </form>
  </div>
</template>

<style scoped></style>
