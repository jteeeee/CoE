import type { ReactNode } from "react";
import { Box, Button, Stack, Typography } from "@mui/material";

interface PageHeaderAction {
  label: string;
  icon?: ReactNode;
  onClick: () => void;
  variant?: "text" | "outlined" | "contained";
}

interface PageHeaderProps {
  title: string;
  description: string;
  actions?: PageHeaderAction[];
}

export function PageHeader({ title, description, actions = [] }: PageHeaderProps) {
  return (
    <Stack direction={{ xs: "column", md: "row" }} spacing={2} alignItems={{ xs: "stretch", md: "flex-start" }} justifyContent="space-between">
      <Box sx={{ minWidth: 0 }}>
        <Typography variant="h1" color="text.primary">
          {title}
        </Typography>
        <Typography variant="body1" color="text.secondary" sx={{ mt: 0.75, maxWidth: 780 }}>
          {description}
        </Typography>
      </Box>
      {actions.length > 0 ? (
        <Stack direction="row" spacing={1} justifyContent={{ xs: "flex-start", md: "flex-end" }}>
          {actions.map((action) => (
            <Button key={action.label} variant={action.variant ?? "outlined"} startIcon={action.icon} onClick={action.onClick}>
              {action.label}
            </Button>
          ))}
        </Stack>
      ) : null}
    </Stack>
  );
}
