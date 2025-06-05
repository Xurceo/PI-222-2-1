import axios from "axios";
import type { ICategory } from "../models/types/Category.ts";
import api from "../lib/axios.ts";
import type { ILot } from "../models/types/Lot.ts";

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

export async function getCategoryLots(id: string): Promise<ILot[]> {
  try {
    const response = await api.get(`/categories/${id}/lots`);
    return response.data as ILot[]; // Array of lots.
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
      throw new Error("Error fetching category lots.");
    } else {
      console.error("Unexpected error:", error);
      throw new Error("Unexpected error occurred.");
    }
  }
}

export async function getCategorySubcategories(
  id: string
): Promise<ICategory[]> {
  try {
    const response = await api.get(`/categories/${id}/subcategories`);
    return response.data as ICategory[]; // Array of subcategories.
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
      throw new Error("Error fetching category subcategories.");
    } else {
      console.error("Unexpected error:", error);
      throw new Error("Unexpected error occurred.");
    }
  }
}
