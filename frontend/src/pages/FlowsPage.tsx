import { FileDownloadOutlined, Refresh, Bolt, ErrorOutline } from "@mui/icons-material";
import { Grid, Stack } from "@mui/material";
import { useMemo, useState } from "react";
import { DataTable, type DataTableColumn } from "../components/DataTable";
import { MetricCard } from "../components/MetricCard";
import { PageHeader } from "../components/PageHeader";
import { StatusBadge } from "../components/StatusBadge";
import { useFlows } from "../hooks/useGovernanceData";
import type { Flow, InventoryQuery } from "../types/models";
import { exportRowsAsCsv, formatDate, formatPercent } from "../utils/format";

const allOption = { label: "All", value: "" };

export function FlowsPage() {
  const [query, setQuery] = useState<InventoryQuery>({});
  const { data = [], isLoading, isError, refetch } = useFlows(query);

  const columns = useMemo<DataTableColumn<Flow>[]>(
    () => [
      {
        id: "flowName",
        label: "Flow Name",
        minWidth: 260,
        format: (row) => row.flowName,
        sortValue: (row) => row.flowName
      },
      {
        id: "ownerName",
        label: "Owner",
        minWidth: 180,
        format: (row) => row.ownerName,
        sortValue: (row) => row.ownerName
      },
      {
        id: "lastRunDate",
        label: "Last Run",
        minWidth: 150,
        format: (row) => formatDate(row.lastRunDate),
        sortValue: (row) => new Date(row.lastRunDate).getTime()
      },
      {
        id: "successRate",
        label: "Success Rate",
        align: "right",
        format: (row) => formatPercent(row.successRate, 1),
        sortValue: (row) => row.successRate
      },
      {
        id: "failureCount",
        label: "Failures",
        align: "right",
        format: (row) => row.failureCount,
        sortValue: (row) => row.failureCount
      },
      {
        id: "status",
        label: "Status",
        format: (row) => <StatusBadge value={row.status} />,
        sortValue: (row) => row.status
      }
    ],
    []
  );

  const ownerOptions = useMemo(
    () => [allOption, ...Array.from(new Set(data.map((item) => item.ownerName))).map((value) => ({ label: value, value }))],
    [data]
  );

  const handleExport = () => {
    exportRowsAsCsv(
      "flows.csv",
      data.map((item) => ({
        flowName: item.flowName,
        owner: item.ownerName,
        lastRun: formatDate(item.lastRunDate),
        successRate: item.successRate,
        failures: item.failureCount,
        status: item.status
      }))
    );
  };

  return (
    <Stack spacing={3}>
      <PageHeader
        title="Flows"
        description="Monitor Power Automate ownership, execution success rates, failure counts and lifecycle state."
        actions={[
          { label: "Export", icon: <FileDownloadOutlined />, onClick: handleExport },
          { label: "Refresh", icon: <Refresh />, onClick: () => void refetch() }
        ]}
      />

      <Grid container spacing={3}>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Flows" value={data.length} trend="Filtered inventory" icon={<Bolt />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Failures" value={data.reduce((total, item) => total + item.failureCount, 0)} trend="Current sample window" tone="warning" icon={<ErrorOutline />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Suspended" value={data.filter((item) => item.status === "Suspended").length} trend="Needs triage" tone="critical" icon={<ErrorOutline />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Reliable" value={data.filter((item) => item.successRate >= 98).length} trend="98% or higher" tone="positive" icon={<Bolt />} />
        </Grid>
      </Grid>

      <DataTable
        title="Flow inventory"
        rows={data}
        columns={columns}
        getRowId={(row) => row.flowId}
        searchValue={query.search ?? ""}
        onSearchChange={(value) => setQuery((current) => ({ ...current, search: value }))}
        searchPlaceholder="Search flows or owners"
        filters={[
          {
            label: "Status",
            value: query.status ?? "",
            options: [
              allOption,
              { label: "Active", value: "Active" },
              { label: "Suspended", value: "Suspended" }
            ],
            onChange: (value) => setQuery((current) => ({ ...current, status: value || undefined }))
          },
          {
            label: "Owner",
            value: query.owner ?? "",
            options: ownerOptions,
            onChange: (value) => setQuery((current) => ({ ...current, owner: value || undefined }))
          }
        ]}
        loading={isLoading}
        error={isError}
        emptyTitle="No flows found"
        emptyDescription="Adjust the search or filters to find matching Power Automate flows."
        onRefresh={() => void refetch()}
        onExport={handleExport}
      />
    </Stack>
  );
}
