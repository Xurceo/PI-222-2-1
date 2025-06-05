import axios from "axios";
import api from "../lib/axios.ts";
import type { IBid } from "../models/types/Bid.ts";

export async function placeBid(lotId: string, amount: number): Promise<string> {
  try {
    const res = await api.post(`/bids`, { lotId, amount });
    return res.data as string; // "Bid placed successfully."
  } catch (error) {
    if (axios.isAxiosError(error)) {
      const message = error.response?.data;
      return typeof message === "string" ? message : "Error placing bid.";
    } else {
      return "Unexpected error occurred.";
    }
  }
}

export async function getLotBids(lotId: string): Promise<IBid[]> {
  try {
    const res = await api.get(`/bids/${lotId}`);
    return res.data as IBid[]; // Array of bid IDs.
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
