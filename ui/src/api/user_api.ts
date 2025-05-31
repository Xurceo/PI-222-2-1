import type { IUser } from "../models/types/User.ts";
import api from "../lib/axios.ts";
import type { IRegisterUser } from "../models/types/RegisterUser.ts";

export async function login(
  username: string,
  password: string
): Promise<IUser> {
  const res = await api.post("/auth/login", { username, password }); // Cookie set on backend
  return res.data.user as IUser;
}

export async function logout(): Promise<void> {
  await api.post("/auth/logout");
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

export default api;
