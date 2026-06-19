import type { ReactNode } from "react";
import { InboxOutlined } from "@mui/icons-material";
import { Button, Stack, Typography } from "@mui/material";

interface EmptyStateProps {
  title: string;
  description: string;
  actionLabel?: string;
  onAction?: () => void;
  icon?: ReactNode;
}

export function EmptyState({ title, description, actionLabel, onAction, icon = <InboxOutlined /> }: EmptyStateProps) {
  return (
    <Stack spacing={1.5} alignItems="center" justifyContent="center" sx={{ minHeight: 240, textAlign: "center", px: 2 }}>
      {icon}
      <Typography variant="h2">{title}</Typography>
      <Typography variant="body2" color="text.secondary" sx={{ maxWidth: 420 }}>
        {description}
      </Typography>
      {actionLabel && onAction ? (
        <Button variant="outlined" onClick={onAction}>
          {actionLabel}
        </Button>
      ) : null}
    </Stack>
  );
}
