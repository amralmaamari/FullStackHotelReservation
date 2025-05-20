 /** @type {import('tailwindcss').Config} */
 export default {
  content: ["./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",],
  theme: {
    extend: {
      colors: {
        primary: '#003b95',                // Blue: Brand's main color
        secondray: '#1a73e8',                // Blue: Brand's main color
        onPrimary: '#ffffff',              // Text color on primary background
        hotelGold: '#ffcc00',
        bgPrimary: '##f5f5f5',
      },
      fontFamily: {
        sans: ['Inter', 'sans-serif'],

      },
    },
  },
  plugins: [],
}