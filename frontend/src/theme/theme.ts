import { alpha, createTheme } from "@mui/material/styles";
import type { PaletteMode } from "@mui/material";

export const createAppTheme = (mode: PaletteMode) =>
  createTheme({
    palette: {
      mode,
      primary: {
        main: "#2563eb",
        dark: "#1d4ed8",
        light: "#60a5fa"
      },
      secondary: {
        main: "#0f766e"
      },
      success: {
        main: "#15803d"
      },
      warning: {
        main: "#b45309"
      },
      error: {
        main: "#b91c1c"
      },
      background: {
        default: mode === "light" ? "#f6f8fb" : "#0f172a",
        paper: mode === "light" ? "#ffffff" : "#111827"
      },
      divider: mode === "light" ? alpha("#0f172a", 0.08) : alpha("#e5e7eb", 0.12)
    },
    shape: {
      borderRadius: 12
    },
    typography: {
      fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
      h1: {
        fontSize: "1.875rem",
        fontWeight: 700,
        lineHeight: 1.2
      },
      h2: {
        fontSize: "1.25rem",
        fontWeight: 700,
        lineHeight: 1.25
      },
      h3: {
        fontSize: "1rem",
        fontWeight: 700,
        lineHeight: 1.3
      },
      button: {
        textTransform: "none",
        fontWeight: 600
      }
    },
    components: {
      MuiCard: {
        styleOverrides: {
          root: ({ theme }) => ({
            borderRadius: 16,
            boxShadow:
              theme.palette.mode === "light"
                ? "0 8px 24px rgba(15, 23, 42, 0.06)"
                : "0 8px 24px rgba(0, 0, 0, 0.24)"
          })
        }
      },
      MuiButton: {
        defaultProps: {
          disableElevation: true
        }
      },
      MuiTableCell: {
        styleOverrides: {
          head: ({ theme }) => ({
            backgroundColor:
              theme.palette.mode === "light" ? alpha(theme.palette.primary.main, 0.05) : alpha(theme.palette.primary.light, 0.08),
            color: theme.palette.text.secondary,
            fontWeight: 700
          })
        }
      },
      MuiTooltip: {
        defaultProps: {
          arrow: true
        }
      }
    }
  });
