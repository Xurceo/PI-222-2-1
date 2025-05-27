<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import type { ICategory } from "../../types/Category.ts"
import type { ILot } from "../../types/Lot.ts";
import {fetchCategoryById, AddCategory, UpdateCategory} from "../../api/category_api.ts";
import { fetchLots } from "../../api/lot_api.ts";

const props = defineProps<{
  id: string
}>()

const name = ref<string>('');
const lots = ref<ILot[]>([]);
const category = ref<ICategory>();
const showSuggestions = ref(false);
const ignoreBlur = ref(false)

// Filter lots based on input
const filteredLots = computed(() => {
  if (!name.value) return [];
  return lots.value.filter(lot =>
      lot.title.toLowerCase().includes(name.value.toLowerCase())
  );
});

onMounted(async () => {
  category.value = await fetchCategoryById(parseInt(props.id));
  lots.value = await fetchLots();
});

const selectLot = (lotTitle: string) => {
  name.value = lotTitle;
  showSuggestions.value = false;
};

const submit = async () => {
  if (!name.value.trim()) {
    alert('Name is required');
    return;
  }

  const lot = lots.value.find(lot => lot.title === name.value);
  if (!lot) {
    alert('Lot not found');
    return;
  }

  lot.categoryId = category.value!.id;
  category.value!.lots.push(lot);
  console.log(JSON.stringify(category.value));
  await UpdateCategory(category.value as ICategory);
  name.value = ''; // Clear input after submission
};

const handleInputFocus = () => {
  showSuggestions.value = true
}

const handleInputBlur = () => {
  if (!ignoreBlur.value) {
    showSuggestions.value = false
  }
}

const handleMouseDown = () => {
  ignoreBlur.value = true
  setTimeout(() => {
    ignoreBlur.value = false
  }, 100)
}
</script>

<template>
  <div class="flex-1/2 relative">
    <h1 class="text-black m-20">Add Lot To Category</h1>
    <div class="flex justify-center m-10">
      <div class="relative">
        <input
            class="bg-white hover:bg-neutral-200 duration-300 text-black h-12 w-96 p-2 m-1 border-gray-600 border-2 rounded-lg"
            v-model="name"
            type="text"
            placeholder="Enter Lot Name"
            @focus="handleInputFocus"
            @blur="handleInputBlur"
            @input="showSuggestions = true"
        />

        <ul
            v-if="showSuggestions && filteredLots.length"
            class="absolute z-10 mt-1 w-fit bg-white border border-gray-300 rounded-lg shadow-lg max-h-60 overflow-auto"
            @mousedown="handleMouseDown"
        >
          <li
              v-for="lot in filteredLots"
              :key="lot.id"
              class="p-2 hover:bg-gray-100 text-black cursor-pointer"
              @mousedown="selectLot(lot.title)"
          >
            {{ lot.title }}
          </li>
        </ul>
      </div>

      <button
          class="border-2 rounded-lg border-gray-600 h-12 w-48 text-xl text-black m-2 cursor-pointer bg-amber-100 hover:bg-amber-200 duration-300"
          @click="submit"
      >
        Add Lot
      </button>
    </div>
  </div>
</template>