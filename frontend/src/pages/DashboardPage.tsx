import {
  AccountTree,
  Apps,
  ErrorOutline,
  Groups,
  Refresh,
  ShieldOutlined,
  WarningAmber,
  Bolt
} from "@mui/icons-material";
import { Box, Card, CardContent, Grid, LinearProgress, List, ListItem, ListItemText, Skeleton, Stack, Typography } from "@mui/material";
import { useTheme } from "@mui/material/styles";
import { Cell, Line, LineChart, Pie, PieChart, ResponsiveContainer, Tooltip, XAxis, YAxis, Bar, BarChart } from "recharts";
import { HealthIndicator } from "../components/HealthIndicator";
import { MetricCard } from "../components/MetricCard";
import { PageHeader } from "../components/PageHeader";
import { RiskBadge } from "../components/RiskBadge";
import { TrendCard } from "../components/TrendCard";
import { ErrorState } from "../components/ErrorState";
import { useDashboardSummary } from "../hooks/useGovernanceData";
import { formatDateTime, formatNumber, formatPercent } from "../utils/format";

export function DashboardPage() {
  const theme = useTheme();
  const { data, isLoading, isError, refetch } = useDashboardSummary();
  const chartPalette = [theme.palette.primary.main, theme.palette.secondary.main, theme.palette.warning.main, theme.palette.error.main];

  if (isLoading) {
    return (
      <Stack spacing={3}>
        <PageHeader title="Dashboard" description="Platform scale, operational health, governance coverage and risk exposure." />
        <Grid container spacing={3}>
          {Array.from({ length: 8 }).map((_, index) => (
            <Grid item xs={12} sm={6} xl={3} key={index}>
              <Skeleton variant="rounded" height={156} />
            </Grid>
          ))}
        </Grid>
      </Stack>
    );
  }

  if (isError || !data) {
    return <ErrorState title="Unable to load dashboard" onRetry={() => void refetch()} />;
  }

  return (
    <Stack spacing={3}>
      <PageHeader
        title="Dashboard"
        description="Executive view of Power Platform scale, ownership coverage, operational health and governance risk."
        actions={[{ label: "Refresh", icon: <Refresh />, onClick: () => void refetch() }]}
      />

      <Grid container spacing={3}>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Applications" value={formatNumber(data.metrics.totalApplications)} trend="+14 this month" tone="positive" icon={<Apps />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Flows" value={formatNumber(data.metrics.totalFlows)} trend="+33 this month" tone="positive" icon={<Bolt />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Environments" value={data.metrics.totalEnvironments} trend="2 production estates" icon={<AccountTree />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Makers" value={formatNumber(data.metrics.totalMakers)} trend="+9 active makers" tone="positive" icon={<Groups />} />
        </Grid>
      </Grid>

      <Grid container spacing={3}>
        <Grid item xs={12} xl={8}>
          <TrendCard title="Operational health" description="Application and flow growth with recent execution failures.">
            <ResponsiveContainer width="100%" height="100%">
              <LineChart data={data.trends}>
                <XAxis dataKey="period" />
                <YAxis />
                <Tooltip />
                <Line type="monotone" dataKey="applications" stroke={chartPalette[0]} strokeWidth={3} dot={false} />
                <Line type="monotone" dataKey="flows" stroke={chartPalette[1]} strokeWidth={3} dot={false} />
                <Line type="monotone" dataKey="failures" stroke={chartPalette[3]} strokeWidth={2} dot={false} />
              </LineChart>
            </ResponsiveContainer>
          </TrendCard>
        </Grid>
        <Grid item xs={12} xl={4}>
          <TrendCard title="Environment distribution" description="Asset concentration by environment type.">
            <ResponsiveContainer width="100%" height="100%">
              <PieChart>
                <Pie data={data.environmentDistribution} dataKey="value" nameKey="name" outerRadius={94} label>
                  {data.environmentDistribution.map((entry, index) => (
                    <Cell key={entry.name} fill={chartPalette[index % chartPalette.length]} />
                  ))}
                </Pie>
                <Tooltip />
              </PieChart>
            </ResponsiveContainer>
          </TrendCard>
        </Grid>
      </Grid>

      <Grid container spacing={3}>
        <Grid item xs={12} md={4}>
          <Card sx={{ height: "100%" }}>
            <CardContent>
              <Stack spacing={2.5}>
                <HealthIndicator value={data.health.platformHealth} caption="Current platform signal" />
                {[
                  ["Ownership coverage", data.health.ownershipCoveragePercent],
                  ["Governance compliance", data.health.governanceCompliancePercent],
                  ["Review completion", data.health.reviewCompletionPercent],
                  ["Capacity utilisation", data.health.capacityUtilisationPercent]
                ].map(([label, value]) => (
                  <Box key={label}>
                    <Stack direction="row" justifyContent="space-between" sx={{ mb: 0.75 }}>
                      <Typography variant="body2" fontWeight={700}>
                        {label}
                      </Typography>
                      <Typography variant="body2">{formatPercent(Number(value))}</Typography>
                    </Stack>
                    <LinearProgress variant="determinate" value={Number(value)} sx={{ height: 8, borderRadius: 8 }} />
                  </Box>
                ))}
              </Stack>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} md={4}>
          <TrendCard title="Governance reviews" description="Completed review activity by month.">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={data.trends}>
                <XAxis dataKey="period" />
                <YAxis />
                <Tooltip />
                <Bar dataKey="reviews" fill={chartPalette[1]} radius={[6, 6, 0, 0]} />
              </BarChart>
            </ResponsiveContainer>
          </TrendCard>
        </Grid>
        <Grid item xs={12} md={4}>
          <Card sx={{ height: "100%" }}>
            <CardContent>
              <Stack spacing={2}>
                <Typography variant="h2">Risk exposure</Typography>
                <Stack spacing={1.5}>
                  <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <Stack direction="row" spacing={1} alignItems="center">
                      <WarningAmber color="warning" />
                      <Typography variant="body2">Orphaned applications</Typography>
                    </Stack>
                    <RiskBadge value="High" />
                  </Stack>
                  <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <Stack direction="row" spacing={1} alignItems="center">
                      <ShieldOutlined color="error" />
                      <Typography variant="body2">High risk assets</Typography>
                    </Stack>
                    <Typography variant="h2">{formatNumber(61)}</Typography>
                  </Stack>
                  <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <Stack direction="row" spacing={1} alignItems="center">
                      <ErrorOutline color="error" />
                      <Typography variant="body2">Failed flows in 7 days</Typography>
                    </Stack>
                    <Typography variant="h2">{data.metrics.failedFlowsLastSevenDays}</Typography>
                  </Stack>
                </Stack>
              </Stack>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      <Card>
        <CardContent>
          <Stack spacing={1.5}>
            <Typography variant="h2">Recent activity</Typography>
            <List disablePadding>
              {data.recentActivity.map((item) => (
                <ListItem key={item.id} disableGutters divider>
                  <ListItemText
                    primary={item.title}
                    secondary={`${item.detail} Recorded ${formatDateTime(item.occurredAt)}.`}
                    primaryTypographyProps={{ fontWeight: 700 }}
                  />
                </ListItem>
              ))}
            </List>
          </Stack>
        </CardContent>
      </Card>
    </Stack>
  );
}
