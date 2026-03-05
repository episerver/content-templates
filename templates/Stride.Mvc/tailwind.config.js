/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Views/**/*.cshtml",
    "./Components/**/*.cs",
    "./Models/**/*.cs",
    "./wwwroot/js/**/*.js"
  ],
  theme: {
    container: {
      center: true,
      padding: '1.5rem', // px-6
      screens: {
        DEFAULT: '1200px', // max-width: 1200px
      },
    },
    fontFamily: {
      sans: ['"Inter"', '"Segoe UI"', 'Roboto', '"Helvetica Neue"', 'sans-serif'],
    },
    extend: {
      maxWidth: {
        'half-col': '35.25rem', // 564px - matches grid column width: (1152px - 24px gap) / 2
      },
      backgroundImage: {
        'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
      },
      colors: {
        background: '#ffffff', // White - footer, cards
        background2: '#f2f8fc', // Light blue-gray - page body
        foreground: '#000000',
        card: '#00000006',
        key1: '#0980f6', // Primary blue gradient color
        key2: '#0980f6', // Secondary blue gradient color
        primary: {
          DEFAULT: '#0066cc',
          50: '#e6f2ff',
          100: '#b3d9ff',
          200: '#80c0ff',
          300: '#4da6ff',
          400: '#1a8dff',
          500: '#0066cc',
          600: '#0052a3',
          700: '#003d7a',
          800: '#002952',
          900: '#001429',
        },
      },
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/typography'),
  ],
}
