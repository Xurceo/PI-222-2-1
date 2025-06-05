import type { ILot } from "../models/types/Lot.ts";
import type { ICreateLot } from "../models/types/CreateLot.ts";
import api from "../lib/axios.ts";
import axios from "axios";

export async function addLot(lot: ICreateLot): Promise<void> {
  try {
    await api.post(`/lots`, lot);
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
    } else {
      console.error("Unexpected error:", error);
    }
  }
}

export async function getLots(): Promise<ILot[]> {
  try {
    const response = await api.get(`/lots`);
    return response.data as ILot[];
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
    } else {
      console.error("Unexpected error:", error);
    }
    throw error;
  }
}

export async function getLotById(id: string): Promise<ILot> {
  try {
    const response = await api.get(`/lots/${id}`);
    return response.data as ILot;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
    } else {
      console.error("Unexpected error:", error);
    }
    throw error;
  }
}
