//colori,fonts,theme,spacings ect personalizzati applicati a tailwind da cui poi verranno costruite le utility
// /** @type {import('tailwindcss').Config} */

import type { Config } from 'tailwindcss'
const config: Config = {   //uso di ESM syntax (x -v 10.x+)
  content: [
    "./src/**/*.{js,ts,jsx,tsx}", // aggiorna il path al tuo progetto
  ],
  theme: {
    extend: {
      fontFamily: {
        //Source Sans 3 come sans principale, ora verra applicato e.g.font-sans text-xl font-bold (non necessario 'font-sans' se lo hai gia nel :root cmnq in futuro se cambi font root cambiarera tutto mentre se qui ovverride default con target font esso rimarra override)
        sans: ['"Source Sans 3"', 'Inter', 'Helvetica', 'Arial', 'sans-serif'],
      },
      backgroundImage: {
        //'mylandingpage': "url('/images/earth.jpg')",
      },
      colors: {
        myblue: {
            DEFAULT: '#0a66c2',  //blue linkedin
            hover: '#004182',  //blue hover linkedin
        },
        myorange: {
            DEFAULT: '#ff6f06',
            hover: '#e64700'
        }
      },
    },
  },
  plugins: [],
};

export default config;