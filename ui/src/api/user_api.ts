import type { IUser } from "../models/types/User.ts";
import api from "../lib/axios.ts";
import type { IRegisterUser } from "../models/types/RegisterUser.ts";
import type { ILot } from "../models/types/Lot.ts";
import type { IBid } from "../models/types/Bid.ts";

export async function apiLogin(
  username: string,
  password: string
): Promise<IUser> {
  const res = await api.post("/auth/login", { username, password }); // Cookie set on backend
  return res.data.user as IUser;
}

export async function getCurrentUser(): Promise<IUser> {
  const res = await api.get("/auth/me"); // Cookie sent automatically
  return res.data.user as IUser;
}

export async function getUserById(id: string): Promise<IUser> {
  const res = await api.get(`/users/${id}`);
  return res.data as IUser;
}

export async function getAllUsers(): Promise<IUser[]> {
  const res = await api.get("/users");
  return res.data as IUser[];
}

export async function registerUser(user: IRegisterUser): Promise<IUser> {
  const res = await api.post("/auth/register", user);
  return res.data.user as IUser;
}

export async function apiLogout() {
  await api.post("/auth/logout");
}

export async function getUserLots(id: string): Promise<ILot[]> {
  const res = await api.get(`/users/${id}/lots`);
  return res.data as ILot[]; // Array of lots
}

export async function getUserBids(id: string): Promise<IBid[]> {
  const res = await api.get(`/users/${id}/bids`);
  return res.data as IBid[]; // Array of bids
}
