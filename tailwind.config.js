import colors from "tailwindcss/colors";
// The reason that tailwind lsp is not working is this line

const secondary = colors.violet;
const accent = colors.orange;

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    // get all the cshtml files
    "./Pages/**/*.cshtml",
    "./Views/**/*.cshtml",
    // get all the static js files
    "./wwwroot/**/*.js",
  ],
  theme: {
    extend: {
      maxWidth: {
        content: "100rem",
      },
      gridTemplateColumns: {
        fluid: "repeat(auto-fit, minmax(20rem, 1fr))",
      },
      colors: {
        primary: {
          50: "#f7f6f9",
          100: "#efedf1",
          200: "#dad7e0",
          300: "#b9b4c5",
          400: "#928ba5",
          500: "#756d8a",
          600: "#5f5772",
          700: "#4e475d",
          800: "#433e4e",
          900: "#3b3644",
          950: "#242129",
          DEFAULT: "#242129",
        },
        secondary: {
          // make an alias for the secondary color
          // add a default value as well
          ...secondary,
          DEFAULT: secondary[500],
        },
        accent: {
          // make an alias for the secondary color
          // add a default value as well
          ...accent,
          DEFAULT: accent[300],
        },
      },
      fontFamily: {
        sans: ["Montserrat", "sans-serif"],
        mono: ['"Fira Code"', "monospace"],
      },
      fontSize: {
        header: [
          "clamp(3rem, 10vw, 5.5rem)",
          {
            lineHeight: "1",
          },
        ],
        subheader: [
          "clamp(2.5rem, 8vw, 3rem)",
          {
            lineHeight: "1",
          },
        ],
      },
    },
  },
  plugins: [],
};
