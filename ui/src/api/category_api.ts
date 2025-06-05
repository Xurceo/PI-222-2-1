import axios from "axios";
import type { ICategory } from "../models/types/Category.ts";
import api from "../lib/axios.ts";

export async function addCategory(category: ICategory): Promise<void> {
  try {
    await api.post(`/categories`, category);
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
    } else {
      console.error("Unexpected error:", error);
    }
  }
}

export async function updateCategory(category: ICategory): Promise<void> {
  try {
    await api.post(`/categories/update`, category);
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
    } else {
      console.error("Unexpected error:", error);
    }
  }
}

export async function getCategories(): Promise<ICategory[]> {
  try {
    const response = await api.get(`/categories`);
    return response.data as ICategory[];
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
    } else {
      console.error("Unexpected error:", error);
    }
    throw error;
  }
}

export async function getCategoryById(id: string): Promise<ICategory> {
  try {
    const response = await api.get(`/categories/${id}`);
    return response.data as ICategory;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
    } else {
      console.error("Unexpected error:", error);
    }
    throw error;
  }
}
