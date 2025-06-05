import type { ILot } from "../models/types/Lot.ts";
import api from "../lib/axios.ts";
import axios from "axios";
import type { IBid } from "../models/types/Bid.ts";

export async function addLot(lot: ILot): Promise<void> {
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

export async function getLotBids(id: string): Promise<IBid[]> {
  try {
    const response = await api.get(`/lots/${id}/bids`);
    return response.data as IBid[]; // Array of bids.
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
      throw new Error("Error fetching bids.");
    } else {
      console.error("Unexpected error:", error);
      throw new Error("Unexpected error occurred.");
    }
  }
}

export async function confirmLot(lotId: string): Promise<string> {
  try {
    const res = await api.put(`/lots/${lotId}/confirm`);
    return res.data as string;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
      console.log(error.response);
      throw new Error("Error confirming lot.");
    } else {
      console.error("Unexpected error:", error);
      throw new Error("Unexpected error occurred.");
    }
  }
}
