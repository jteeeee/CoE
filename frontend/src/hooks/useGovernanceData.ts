import { useQuery } from "@tanstack/react-query";
import { governanceService } from "../services/serviceProvider";
import type { InventoryQuery } from "../types/models";

export const useDashboardSummary = () =>
  useQuery({
    queryKey: ["dashboard-summary"],
    queryFn: () => governanceService.getDashboardSummary()
  });

export const useApplications = (query: InventoryQuery = {}) =>
  useQuery({
    queryKey: ["applications", query],
    queryFn: () => governanceService.getApplications(query)
  });

export const useFlows = (query: InventoryQuery = {}) =>
  useQuery({
    queryKey: ["flows", query],
    queryFn: () => governanceService.getFlows(query)
  });

export const useEnvironments = (query: InventoryQuery = {}) =>
  useQuery({
    queryKey: ["environments", query],
    queryFn: () => governanceService.getEnvironments(query)
  });

export const useGovernanceRecords = () =>
  useQuery({
    queryKey: ["governance-records"],
    queryFn: () => governanceService.getGovernanceRecords()
  });

export const useRiskRecords = () =>
  useQuery({
    queryKey: ["risk-records"],
    queryFn: () => governanceService.getRiskRecords()
  });
