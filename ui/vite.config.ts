import { defineConfig, loadEnv } from "vite";
import vue from "@vitejs/plugin-vue";
import tailwindcss from "@tailwindcss/vite";
import fs from "fs";

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd());
  const apiBase = env.VITE_API_URL;
  console.log("API BASE URL:", apiBase);

  return {
    server: {
      https: {
        key: fs.readFileSync("localhost-key.pem"),
        cert: fs.readFileSync("localhost.pem"),
      },
      host: env.VITE_HOST || "localhost",
      port: parseInt(env.VITE_PORT, 10) || 3000,
      proxy: {
        "/api": {
          target: apiBase,
          changeOrigin: true,
          secure: false,
        },
      },
    },
    plugins: [vue(), tailwindcss()],
  };
});
