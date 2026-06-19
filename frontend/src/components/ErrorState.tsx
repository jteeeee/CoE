import { ErrorOutline } from "@mui/icons-material";
import { Alert, Button, Stack, Typography } from "@mui/material";

interface ErrorStateProps {
  title?: string;
  description?: string;
  onRetry?: () => void;
}

export function ErrorState({
  title = "Unable to load data",
  description = "Please try again later. If the issue continues, contact the platform team.",
  onRetry
}: ErrorStateProps) {
  return (
    <Alert severity="error" icon={<ErrorOutline />} sx={{ alignItems: "center" }}>
      <Stack direction={{ xs: "column", sm: "row" }} spacing={2} alignItems={{ xs: "flex-start", sm: "center" }}>
        <Stack spacing={0.5}>
          <Typography variant="body1" fontWeight={700}>
            {title}
          </Typography>
          <Typography variant="body2">{description}</Typography>
        </Stack>
        {onRetry ? (
          <Button color="inherit" variant="outlined" onClick={onRetry}>
            Retry
          </Button>
        ) : null}
      </Stack>
    </Alert>
  );
}
