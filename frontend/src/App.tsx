import { useMemo, useState } from "react";
import { ThemeProvider } from "@mui/material/styles";
import type { PaletteMode } from "@mui/material";
import { Navigate, Route, Routes } from "react-router-dom";
import { ApplicationShell } from "./layouts/ApplicationShell";
import { ApplicationsPage } from "./pages/ApplicationsPage";
import { DashboardPage } from "./pages/DashboardPage";
import { EnvironmentsPage } from "./pages/EnvironmentsPage";
import { FlowsPage } from "./pages/FlowsPage";
import { createAppTheme } from "./theme/theme";

export default function App() {
  const [mode, setMode] = useState<PaletteMode>("light");
  const theme = useMemo(() => createAppTheme(mode), [mode]);

  return (
    <ThemeProvider theme={theme}>
      <ApplicationShell mode={mode} onToggleMode={() => setMode((current) => (current === "light" ? "dark" : "light"))}>
        <Routes>
          <Route path="/" element={<Navigate to="/dashboard" replace />} />
          <Route path="/dashboard" element={<DashboardPage />} />
          <Route path="/applications" element={<ApplicationsPage />} />
          <Route path="/flows" element={<FlowsPage />} />
          <Route path="/environments" element={<EnvironmentsPage />} />
          <Route path="*" element={<Navigate to="/dashboard" replace />} />
        </Routes>
      </ApplicationShell>
    </ThemeProvider>
  );
}
