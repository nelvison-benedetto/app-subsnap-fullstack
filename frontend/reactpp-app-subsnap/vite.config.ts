import { defineConfig } from 'vite'
import path from "path"   //x path in modo cross-platform (Windows / Mac / Linux)
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'  //necessario plugin @tailwindcss/vites

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    react(),
    tailwindcss(),
  ],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),  //x import Button from '../../components/Button' -> import Button from '@/components/Button'
    },
  },
});

//pero x big prj serio dovresti forse sfruttare solo postcss (xk tailwind cmnq nasce proprio da postcss)
/*
import { defineConfig } from 'vite'
import path from 'path'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
})

// postcss.config.js
module.exports = {
  plugins: {
    tailwindcss: {},
    autoprefixer: {},
  },
}
*/