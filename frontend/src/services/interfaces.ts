import type {
  Application,
  DashboardSummary,
  Environment,
  Flow,
  GovernanceRecord,
  InventoryQuery,
  RiskRecord
} from "../types/models";

export interface GovernanceApi {
  getDashboardSummary(): Promise<DashboardSummary>;
  getApplications(query?: InventoryQuery): Promise<Application[]>;
  getFlows(query?: InventoryQuery): Promise<Flow[]>;
  getEnvironments(query?: InventoryQuery): Promise<Environment[]>;
  getGovernanceRecords(): Promise<GovernanceRecord[]>;
  getRiskRecords(): Promise<RiskRecord[]>;
}
