import type { IUser } from "../types/User.ts";
import api from "../lib/axios.ts";

export async function login(
  username: string,
  password: string
): Promise<IUser> {
  const res = await api.post("/auth/login", { username, password }); // Cookie set on backend
  return res.data as IUser;
}

export async function logout(): Promise<void> {
  await api.post("/auth/logout");
}

export async function getCurrentUser(): Promise<IUser> {
  const res = await api.get("/auth/me"); // Cookie sent automatically
  return res.data as IUser;
}

export async function getUserById(id: number): Promise<IUser> {
  const res = await api.get(`/users/${id}`);
  return res.data as IUser;
}

export default api;
