import { Chip } from "@mui/material";
import type { RiskLevel } from "../types/models";

interface RiskBadgeProps {
  value: RiskLevel;
}

const riskColor = {
  Low: "success",
  Medium: "warning",
  High: "error",
  Critical: "error"
} as const;

export function RiskBadge({ value }: RiskBadgeProps) {
  return <Chip size="small" label={value} color={riskColor[value]} variant={value === "Critical" ? "filled" : "outlined"} />;
}
