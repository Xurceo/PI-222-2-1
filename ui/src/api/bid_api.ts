import axios from "axios";
import type { IBid } from "../models/types/Bid.ts";
import api from "../lib/axios.ts";

export async function placeBid(lotId: string, amount: number): Promise<void> {
  try {
    await api.post(`/bids`, { lotId, amount });
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.response?.data || error.message);
    } else {
      console.error("Unexpected error:", error);
    }
  }
}
