import type { ReactNode } from "react";
import { ArrowDownward, ArrowUpward } from "@mui/icons-material";
import { Card, CardContent, Stack, Typography } from "@mui/material";
import { alpha, useTheme } from "@mui/material/styles";

interface MetricCardProps {
  title: string;
  value: string | number;
  trend: string;
  tone?: "neutral" | "positive" | "warning" | "critical";
  icon: ReactNode;
}

const toneToColor = {
  neutral: "primary.main",
  positive: "success.main",
  warning: "warning.main",
  critical: "error.main"
} as const;

export function MetricCard({ title, value, trend, tone = "neutral", icon }: MetricCardProps) {
  const theme = useTheme();
  const color = toneToColor[tone];
  const TrendIcon = tone === "critical" || tone === "warning" ? ArrowDownward : ArrowUpward;

  return (
    <Card>
      <CardContent>
        <Stack direction="row" alignItems="flex-start" justifyContent="space-between" spacing={2}>
          <Stack spacing={1}>
            <Typography variant="body2" color="text.secondary" fontWeight={700}>
              {title}
            </Typography>
            <Typography variant="h1">{value}</Typography>
          </Stack>
          <Stack
            alignItems="center"
            justifyContent="center"
            sx={{
              width: 44,
              height: 44,
              borderRadius: 3,
              bgcolor: alpha(theme.palette[tone === "neutral" ? "primary" : tone === "positive" ? "success" : tone === "warning" ? "warning" : "error"].main, 0.1),
              color
            }}
          >
            {icon}
          </Stack>
        </Stack>
        <Stack direction="row" spacing={0.75} alignItems="center" sx={{ mt: 2, color }}>
          <TrendIcon fontSize="small" />
          <Typography variant="caption" fontWeight={700}>
            {trend}
          </Typography>
        </Stack>
      </CardContent>
    </Card>
  );
}
