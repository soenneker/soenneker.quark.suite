export default {
  content: [
    "./TailwindElements.txt",
    "./**/*.txt",
    "../**/*.razor",
    "../**/*.cshtml",
    "../**/*.html"
  ],
  theme: {
    extend: {
      colors: {
        alert: {
          success: {
            DEFAULT: "var(--alert-success)",
            foreground: "var(--alert-success-foreground)",
            bg: "var(--alert-success-bg)",
          },
          info: {
            DEFAULT: "var(--alert-info)",
            foreground: "var(--alert-info-foreground)",
            bg: "var(--alert-info-bg)",
          },
          warning: {
            DEFAULT: "var(--alert-warning)",
            foreground: "var(--alert-warning-foreground)",
            bg: "var(--alert-warning-bg)",
          },
          danger: {
            DEFAULT: "var(--alert-danger)",
            foreground: "var(--alert-danger-foreground)",
            bg: "var(--alert-danger-bg)",
          },
        },
      },
    },
  },
  plugins: [
  require("tw-animate-css")
]
};
