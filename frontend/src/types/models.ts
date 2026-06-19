export type AssetStatus = "Active" | "Inactive" | "Draft" | "Suspended";

export type RiskLevel = "Low" | "Medium" | "High" | "Critical";

export type Criticality = "Low" | "Medium" | "High" | "Business Critical";

export type HealthState = "Healthy" | "Degraded" | "At Risk" | "Unknown";

export interface Application {
  appId: string;
  appName: string;
  environmentId: string;
  environmentName: string;
  ownerId: string;
  ownerName: string;
  ownerEmail: string;
  createdDate: string;
  modifiedDate: string;
  lastUsedDate: string;
  userCount: number;
  status: AssetStatus;
  riskRating: RiskLevel;
  businessCriticality: Criticality;
  supportTeam: string;
}

export interface Flow {
  flowId: string;
  flowName: string;
  environmentId: string;
  ownerId: string;
  ownerName: string;
  createdDate: string;
  modifiedDate: string;
  lastRunDate: string;
  failureCount: number;
  successRate: number;
  status: AssetStatus;
}

export interface Environment {
  environmentId: string;
  environmentName: string;
  environmentType: "Production" | "Sandbox" | "Developer" | "Default";
  region: string;
  capacityUsedMb: number;
  capacityAllocatedMb: number;
  appCount: number;
  flowCount: number;
  makerCount: number;
  dlpPolicyName: string;
}

export interface GovernanceRecord {
  governanceId: string;
  assetType: "Application" | "Flow";
  assetId: string;
  businessOwner: string;
  supportTeam: string;
  criticality: Criticality;
  lifecycleStatus: "Active" | "Review Due" | "Retiring" | "Archived";
  reviewDate: string;
  comments: string;
}

export interface RiskRecord {
  riskId: string;
  assetId: string;
  assetType: "Application" | "Flow";
  riskLevel: RiskLevel;
  riskCategory: "Ownership" | "Operational" | "Compliance" | "Security" | "Lifecycle";
  description: string;
  reviewDate: string;
}

export interface DashboardMetric {
  label: string;
  value: number | string;
  trend: string;
  tone: "neutral" | "positive" | "warning" | "critical";
}

export interface TrendPoint {
  period: string;
  applications: number;
  flows: number;
  failures?: number;
  reviews?: number;
}

export interface DistributionPoint {
  name: string;
  value: number;
}

export interface ActivityItem {
  id: string;
  title: string;
  detail: string;
  severity: "Info" | "Warning" | "Critical" | "Success";
  occurredAt: string;
}

export interface DashboardSummary {
  metrics: {
    totalApplications: number;
    totalFlows: number;
    totalEnvironments: number;
    totalMakers: number;
    totalConnectors: number;
    appsWithoutOwners: number;
    disabledOwnerAssets: number;
    inactiveAssets: number;
    failedFlowsLastSevenDays: number;
  };
  health: {
    platformHealth: HealthState;
    ownershipCoveragePercent: number;
    governanceCompliancePercent: number;
    reviewCompletionPercent: number;
    capacityUtilisationPercent: number;
  };
  trends: TrendPoint[];
  environmentDistribution: DistributionPoint[];
  riskDistribution: DistributionPoint[];
  recentActivity: ActivityItem[];
}

export interface InventoryQuery {
  search?: string;
  status?: string;
  environment?: string;
  riskLevel?: string;
  owner?: string;
}

export interface SelectOption {
  label: string;
  value: string;
}
