import { ref } from "vue";
import router from "../router";
import type { IUser } from "../models/types/User";
import { apiLogin, apiLogout, getCurrentUser } from "../api/user_api";

const currentUser = ref<IUser | null>(null);
const loading = ref<boolean>(true);

export function useAuth() {
  // Load user from localStorage on startup
  const loadUser = async () => {
    try {
      currentUser.value = await getCurrentUser();
    } catch (e) {
      currentUser.value = null;
    } finally {
      loading.value = false;
    }
  };

  const login = async (username: string, password: string) => {
    const user = await apiLogin(username, password);
    currentUser.value = user;
    localStorage.setItem("user", JSON.stringify(user));
    return user;
  };

  const logout = async () => {
    await apiLogout();
    currentUser.value = null;
    localStorage.removeItem("user");
  };

  return {
    currentUser,
    loading,
    login,
    logout,
    loadUser,
  };
}
