import { FileDownloadOutlined, Refresh } from "@mui/icons-material";
import { Grid, Stack } from "@mui/material";
import { useMemo, useState } from "react";
import { DataTable, type DataTableColumn } from "../components/DataTable";
import { MetricCard } from "../components/MetricCard";
import { PageHeader } from "../components/PageHeader";
import { RiskBadge } from "../components/RiskBadge";
import { StatusBadge } from "../components/StatusBadge";
import { useApplications } from "../hooks/useGovernanceData";
import type { Application, InventoryQuery } from "../types/models";
import { exportRowsAsCsv, formatDate, formatNumber } from "../utils/format";

const allOption = { label: "All", value: "" };

export function ApplicationsPage() {
  const [query, setQuery] = useState<InventoryQuery>({});
  const { data = [], isLoading, isError, refetch } = useApplications(query);

  const columns = useMemo<DataTableColumn<Application>[]>(
    () => [
      {
        id: "appName",
        label: "App Name",
        minWidth: 240,
        format: (row) => row.appName,
        sortValue: (row) => row.appName
      },
      {
        id: "environmentName",
        label: "Environment",
        minWidth: 220,
        format: (row) => row.environmentName,
        sortValue: (row) => row.environmentName
      },
      {
        id: "ownerName",
        label: "Owner",
        minWidth: 180,
        format: (row) => row.ownerName,
        sortValue: (row) => row.ownerName
      },
      {
        id: "modifiedDate",
        label: "Last Modified",
        minWidth: 150,
        format: (row) => formatDate(row.modifiedDate),
        sortValue: (row) => new Date(row.modifiedDate).getTime()
      },
      {
        id: "lastUsedDate",
        label: "Last Used",
        minWidth: 150,
        format: (row) => formatDate(row.lastUsedDate),
        sortValue: (row) => new Date(row.lastUsedDate).getTime()
      },
      {
        id: "userCount",
        label: "Users",
        align: "right",
        format: (row) => formatNumber(row.userCount),
        sortValue: (row) => row.userCount
      },
      {
        id: "status",
        label: "Status",
        format: (row) => <StatusBadge value={row.status} />,
        sortValue: (row) => row.status
      },
      {
        id: "riskRating",
        label: "Risk",
        format: (row) => <RiskBadge value={row.riskRating} />,
        sortValue: (row) => row.riskRating
      },
      {
        id: "businessCriticality",
        label: "Criticality",
        minWidth: 170,
        format: (row) => row.businessCriticality,
        sortValue: (row) => row.businessCriticality
      },
      {
        id: "supportTeam",
        label: "Support Team",
        minWidth: 200,
        format: (row) => row.supportTeam,
        sortValue: (row) => row.supportTeam
      }
    ],
    []
  );

  const environmentOptions = useMemo(
    () => [allOption, ...Array.from(new Set(data.map((item) => item.environmentName))).map((value) => ({ label: value, value }))],
    [data]
  );

  const ownerOptions = useMemo(
    () => [allOption, ...Array.from(new Set(data.map((item) => item.ownerName))).map((value) => ({ label: value, value }))],
    [data]
  );

  const handleExport = () => {
    exportRowsAsCsv(
      "applications.csv",
      data.map((item) => ({
        appName: item.appName,
        environment: item.environmentName,
        owner: item.ownerName,
        lastModified: formatDate(item.modifiedDate),
        lastUsed: formatDate(item.lastUsedDate),
        users: item.userCount,
        status: item.status,
        risk: item.riskRating,
        criticality: item.businessCriticality,
        supportTeam: item.supportTeam
      }))
    );
  };

  return (
    <Stack spacing={3}>
      <PageHeader
        title="Applications"
        description="Search and manage Power Platform applications, ownership, usage, status and governance risk."
        actions={[
          { label: "Export", icon: <FileDownloadOutlined />, onClick: handleExport },
          { label: "Refresh", icon: <Refresh />, onClick: () => void refetch() }
        ]}
      />

      <Grid container spacing={3}>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Applications" value={formatNumber(data.length)} trend="Filtered inventory" icon={<FileDownloadOutlined />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="High Risk" value={data.filter((item) => item.riskRating === "High" || item.riskRating === "Critical").length} trend="Requires review" tone="warning" icon={<Refresh />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Unassigned" value={data.filter((item) => item.ownerName === "Unassigned").length} trend="Owner missing" tone="critical" icon={<Refresh />} />
        </Grid>
        <Grid item xs={12} sm={6} xl={3}>
          <MetricCard title="Business Critical" value={data.filter((item) => item.businessCriticality === "Business Critical").length} trend="Support mapped" tone="positive" icon={<FileDownloadOutlined />} />
        </Grid>
      </Grid>

      <DataTable
        title="Application inventory"
        rows={data}
        columns={columns}
        getRowId={(row) => row.appId}
        searchValue={query.search ?? ""}
        onSearchChange={(value) => setQuery((current) => ({ ...current, search: value }))}
        searchPlaceholder="Search applications, owners or teams"
        filters={[
          {
            label: "Status",
            value: query.status ?? "",
            options: [
              allOption,
              { label: "Active", value: "Active" },
              { label: "Inactive", value: "Inactive" },
              { label: "Suspended", value: "Suspended" }
            ],
            onChange: (value) => setQuery((current) => ({ ...current, status: value || undefined }))
          },
          {
            label: "Environment",
            value: query.environment ?? "",
            options: environmentOptions,
            onChange: (value) => setQuery((current) => ({ ...current, environment: value || undefined }))
          },
          {
            label: "Risk",
            value: query.riskLevel ?? "",
            options: [
              allOption,
              { label: "Low", value: "Low" },
              { label: "Medium", value: "Medium" },
              { label: "High", value: "High" },
              { label: "Critical", value: "Critical" }
            ],
            onChange: (value) => setQuery((current) => ({ ...current, riskLevel: value || undefined }))
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
        emptyTitle="No applications found"
        emptyDescription="Adjust the search or filters to find matching Power Platform applications."
        onRefresh={() => void refetch()}
        onExport={handleExport}
      />
    </Stack>
  );
}
