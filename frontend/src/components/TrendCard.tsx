import type { ReactNode } from "react";
import { Card, CardContent, Stack, Typography } from "@mui/material";

interface TrendCardProps {
  title: string;
  description?: string;
  action?: ReactNode;
  children: ReactNode;
}

export function TrendCard({ title, description, action, children }: TrendCardProps) {
  return (
    <Card sx={{ height: "100%" }}>
      <CardContent sx={{ height: "100%" }}>
        <Stack spacing={2} sx={{ height: "100%" }}>
          <Stack direction="row" justifyContent="space-between" alignItems="flex-start" spacing={2}>
            <Stack spacing={0.5}>
              <Typography variant="h2">{title}</Typography>
              {description ? (
                <Typography variant="body2" color="text.secondary">
                  {description}
                </Typography>
              ) : null}
            </Stack>
            {action}
          </Stack>
          <Stack sx={{ flex: 1, minHeight: 240 }}>{children}</Stack>
        </Stack>
      </CardContent>
    </Card>
  );
}
