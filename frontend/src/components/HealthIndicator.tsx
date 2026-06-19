import { CheckCircle, ErrorOutline, HelpOutline, WarningAmber } from "@mui/icons-material";
import { Stack, Typography } from "@mui/material";
import type { HealthState } from "../types/models";

interface HealthIndicatorProps {
  value: HealthState;
  caption?: string;
}

const healthConfig = {
  Healthy: { icon: <CheckCircle fontSize="small" />, color: "success.main" },
  Degraded: { icon: <WarningAmber fontSize="small" />, color: "warning.main" },
  "At Risk": { icon: <ErrorOutline fontSize="small" />, color: "error.main" },
  Unknown: { icon: <HelpOutline fontSize="small" />, color: "text.secondary" }
} as const;

export function HealthIndicator({ value, caption }: HealthIndicatorProps) {
  const config = healthConfig[value];

  return (
    <Stack direction="row" spacing={1} alignItems="center" sx={{ color: config.color }}>
      {config.icon}
      <Stack spacing={0}>
        <Typography variant="body2" fontWeight={700}>
          {value}
        </Typography>
        {caption ? (
          <Typography variant="caption" color="text.secondary">
            {caption}
          </Typography>
        ) : null}
      </Stack>
    </Stack>
  );
}
