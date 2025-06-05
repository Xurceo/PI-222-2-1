import axios from "axios";
import api from "../lib/axios.ts";

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
