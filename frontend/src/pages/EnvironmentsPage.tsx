import { AccountTree, FileDownloadOutlined, Refresh, Storage, Groups } from "@mui/icons-material";
import { Box, Grid, LinearProgress, Stack, Typography } from "@mui/material";
import { useMemo, useState } from "react";
import { DataTable, type DataTableColumn } from "../components/DataTable";
import { MetricCard } from "../components/MetricCard";
import { PageHeader } from "../components/PageHeader";
import { StatusBadge } from "../components/StatusBadge";
import { useEnvironments } from "../hooks/useGovernanceData";
import type { Environment, InventoryQuery } from "../types/models";
import { exportRowsAsCsv, formatCapacity, formatNumber } from "../utils/format";

const allOption = { label: "All", value: "" };

export function EnvironmentsPage() {
  const [query, setQuery] = useState<InventoryQuery>({});
  const { data = [], isLoading, isError, refetch } = useEnvironments(query);

  const columns = useMemo<DataTableColumn<Environment>[]>(
    () => [
      {
        id: "environmentName",
        label: "Environment Name",
        minWidth: 260,
        format: (row) => row.environmentName,
        sortValue: (row) => row.environmentName
      },
      {
        id: "environmentType",
        label: "Type",
        format: (row) => <StatusBadge value={row.environmentType} />,
        sortValue: (row) => row.environmentType
      },
      {
        id: "capacity",
        label: "Capacity Usage",
        minWidth: 220,
        format: (row) => {
          const percent = Math.round((row.capacityUsedMb / row.capacityAllocatedMb) * 100);
          return (
            <Box>
              <Stack direction="row" justifyContent="space-between" sx={{ mb: 0.5 }}>
                <Typography variant="body2">{`${formatCapacity(row.capacityUsedMb)} / ${formatCapacity(row.capacityAllocatedMb)}`}</Typography>
                <Typography variant="body2" color="text.secondary">
                  {percent}%
                </Typography>
              </Stack>
              <LinearProgress variant="determinate" value={percent} sx={{ height: 8, borderRadius: 8 }} />
            </Box>
          );
        },
        sortValue: (row) => row.capacityUsedMb / row.capacityAllocatedMb
      },
      {
        id: "appCount",
        label: "Apps",
        align: "right",
        format: (row) => formatNumber(row.appCount),
        sortValue: (row) => row.appCount
      },
      {
        id: "flowCount",
        label: "Flows",
        align: "right",
        format: (row) => formatNumber(row.flowCount),
        sortValue: (row) => row.flowCount
      },
      {
        id: "makerCount",
        label: "Makers",
        align: "right",
        format: (row) => formatNumber(row.makerCount),
        sortValue: (row) => row.makerCount
      },
      {
        id: "dlpPolicyName",
        label: "DLP Policy",
        minWidth: 220,
        format: (row) => row.dlpPolicyName,
        sortValue: (row) => row.dlpPolicyName
      }
    ],
    []
  );

  const handleExport = () => {
    exportRowsAsCsv(
      "environments.csv",
      data.map((item) => ({
        environmentName: item.environmentName,
        type: item.environmentType,
        region: item.region,
        capacityUsed: formatCapacity(item.capacityUsedMb),
        capacityAllocated: formatCapacity(item.capacityAllocatedMb),
        apps: item.appCount,
        flows: item.flowCount,
        makers: item.makerCount,
        dlpPolicy: item.dlpPolicyName
      }))
    );
  };

  const totalCapacity = data.reduce((total, item) => total + item.capacityAllocatedMb, 0);
  const usedCapacity = data.reduce((total, item) => total + item.capacityUsedMb, 0);

  return (
    <Stack spacing={3}>
      <PageHeader
        title="Environments"
        description="Review environment capacity, app and flow concentration, maker activity and DLP policy coverage."
        actions={[
          { label: "Export", icon: <FileDownloadOutlined />, onClick: handleExport },
          { label: "Refresh", icon: <Refresh />, onClick: () => void refetch() }
        ]}
      />

      <Grid container spacing={3}>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Environments" value={data.length} trend="Current estate" icon={<AccountTree />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Production" value={data.filter((item) => item.environmentType === "Production").length} trend="Managed controls" tone="positive" icon={<Storage />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Makers" value={formatNumber(data.reduce((total, item) => total + item.makerCount, 0))} trend="Across environments" icon={<Groups />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Capacity Used" value={`${Math.round((usedCapacity / totalCapacity) * 100)}%`} trend="Tenant allocation" tone="warning" icon={<Storage />} />
        </Grid>
      </Grid>

      <DataTable
        title="Environment inventory"
        rows={data}
        columns={columns}
        getRowId={(row) => row.environmentId}
        searchValue={query.search ?? ""}
        onSearchChange={(value) => setQuery((current) => ({ ...current, search: value }))}
        searchPlaceholder="Search environments, regions or DLP policies"
        filters={[
          {
            label: "Type",
            value: query.status ?? "",
            options: [
              allOption,
              { label: "Production", value: "Production" },
              { label: "Sandbox", value: "Sandbox" },
              { label: "Default", value: "Default" },
              { label: "Developer", value: "Developer" }
            ],
            onChange: (value) => setQuery((current) => ({ ...current, status: value || undefined }))
          }
        ]}
        loading={isLoading}
        error={isError}
        emptyTitle="No environments found"
        emptyDescription="Adjust the search or filters to find matching Power Platform environments."
        onRefresh={() => void refetch()}
        onExport={handleExport}
      />
    </Stack>
  );
}
