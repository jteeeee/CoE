import { Chip } from "@mui/material";
import type { AssetStatus } from "../types/models";

interface StatusBadgeProps {
  value: AssetStatus | string;
}

const statusColor = {
  Active: "success",
  Inactive: "default",
  Draft: "info",
  Suspended: "warning"
} as const;

export function StatusBadge({ value }: StatusBadgeProps) {
  const color = value in statusColor ? statusColor[value as keyof typeof statusColor] : "default";
  return <Chip size="small" label={value} color={color} variant="outlined" />;
}
