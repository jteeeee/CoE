import type {
  ActivityItem,
  Application,
  DashboardSummary,
  Environment,
  Flow,
  GovernanceRecord,
  RiskRecord
} from "../types/models";

export const environments: Environment[] = [
  {
    environmentId: "7c0f2f54-49f7-4b35-9f2c-100000000001",
    environmentName: "Production - Corporate Apps",
    environmentType: "Production",
    region: "Australia Southeast",
    capacityUsedMb: 72400,
    capacityAllocatedMb: 98000,
    appCount: 184,
    flowCount: 312,
    makerCount: 86,
    dlpPolicyName: "Enterprise Business Data"
  },
  {
    environmentId: "7c0f2f54-49f7-4b35-9f2c-100000000002",
    environmentName: "Production - Field Operations",
    environmentType: "Production",
    region: "Australia East",
    capacityUsedMb: 48800,
    capacityAllocatedMb: 64000,
    appCount: 97,
    flowCount: 141,
    makerCount: 42,
    dlpPolicyName: "Field Services Restricted"
  },
  {
    environmentId: "7c0f2f54-49f7-4b35-9f2c-100000000003",
    environmentName: "Sandbox - Innovation Lab",
    environmentType: "Sandbox",
    region: "Australia Southeast",
    capacityUsedMb: 16200,
    capacityAllocatedMb: 40000,
    appCount: 63,
    flowCount: 88,
    makerCount: 55,
    dlpPolicyName: "Maker Trial Guardrails"
  },
  {
    environmentId: "7c0f2f54-49f7-4b35-9f2c-100000000004",
    environmentName: "Default",
    environmentType: "Default",
    region: "Australia Southeast",
    capacityUsedMb: 22500,
    capacityAllocatedMb: 36000,
    appCount: 71,
    flowCount: 104,
    makerCount: 119,
    dlpPolicyName: "Default Environment Controls"
  }
];

export const applications: Application[] = [
  {
    appId: "2f84a9bd-c9f4-4d4b-8404-000000000101",
    appName: "Field Service Permit Hub",
    environmentId: environments[1].environmentId,
    environmentName: environments[1].environmentName,
    ownerId: "u-1001",
    ownerName: "Amelia Hughes",
    ownerEmail: "amelia.hughes@contoso.com",
    createdDate: "2025-07-14T10:00:00Z",
    modifiedDate: "2026-06-12T07:45:00Z",
    lastUsedDate: "2026-06-18T01:10:00Z",
    userCount: 1246,
    status: "Active",
    riskRating: "Medium",
    businessCriticality: "Business Critical",
    supportTeam: "Field Operations Platform",
    },
  {
    appId: "2f84a9bd-c9f4-4d4b-8404-000000000102",
    appName: "Executive KPI Workspace",
    environmentId: environments[0].environmentId,
    environmentName: environments[0].environmentName,
    ownerId: "u-1002",
    ownerName: "Marcus Lee",
    ownerEmail: "marcus.lee@contoso.com",
    createdDate: "2025-10-04T09:30:00Z",
    modifiedDate: "2026-06-17T22:30:00Z",
    lastUsedDate: "2026-06-18T23:15:00Z",
    userCount: 282,
    status: "Active",
    riskRating: "Low",
    businessCriticality: "High",
    supportTeam: "Enterprise Reporting"
  },
  {
    appId: "2f84a9bd-c9f4-4d4b-8404-000000000103",
    appName: "Vendor Onboarding Portal",
    environmentId: environments[0].environmentId,
    environmentName: environments[0].environmentName,
    ownerId: "u-1003",
    ownerName: "Priya Raman",
    ownerEmail: "priya.raman@contoso.com",
    createdDate: "2025-05-20T04:45:00Z",
    modifiedDate: "2026-05-31T11:20:00Z",
    lastUsedDate: "2026-06-15T06:42:00Z",
    userCount: 517,
    status: "Active",
    riskRating: "High",
    businessCriticality: "High",
    supportTeam: "Procurement Systems"
  },
  {
    appId: "2f84a9bd-c9f4-4d4b-8404-000000000104",
    appName: "Facilities Incident Capture",
    environmentId: environments[3].environmentId,
    environmentName: environments[3].environmentName,
    ownerId: "",
    ownerName: "Unassigned",
    ownerEmail: "",
    createdDate: "2024-11-08T02:10:00Z",
    modifiedDate: "2025-12-17T03:00:00Z",
    lastUsedDate: "2026-01-28T23:00:00Z",
    userCount: 73,
    status: "Inactive",
    riskRating: "Critical",
    businessCriticality: "Medium",
    supportTeam: "Facilities Operations"
  },
  {
    appId: "2f84a9bd-c9f4-4d4b-8404-000000000105",
    appName: "Safety Observation Mobile",
    environmentId: environments[1].environmentId,
    environmentName: environments[1].environmentName,
    ownerId: "u-1004",
    ownerName: "Noah Bennett",
    ownerEmail: "noah.bennett@contoso.com",
    createdDate: "2025-03-02T08:45:00Z",
    modifiedDate: "2026-06-10T04:05:00Z",
    lastUsedDate: "2026-06-18T05:22:00Z",
    userCount: 934,
    status: "Active",
    riskRating: "Medium",
    businessCriticality: "Business Critical",
    supportTeam: "Health and Safety"
  },
  {
    appId: "2f84a9bd-c9f4-4d4b-8404-000000000106",
    appName: "Finance Approval Centre",
    environmentId: environments[0].environmentId,
    environmentName: environments[0].environmentName,
    ownerId: "u-1005",
    ownerName: "Grace Tan",
    ownerEmail: "grace.tan@contoso.com",
    createdDate: "2025-01-11T10:30:00Z",
    modifiedDate: "2026-06-04T03:18:00Z",
    lastUsedDate: "2026-06-18T20:05:00Z",
    userCount: 683,
    status: "Active",
    riskRating: "High",
    businessCriticality: "Business Critical",
    supportTeam: "Finance Systems"
  }
];

export const flows: Flow[] = [
  {
    flowId: "f0c5b3a4-884c-4e52-b721-000000000201",
    flowName: "Permit approval orchestration",
    environmentId: environments[1].environmentId,
    ownerId: "u-1001",
    ownerName: "Amelia Hughes",
    createdDate: "2025-07-15T01:00:00Z",
    modifiedDate: "2026-06-16T08:00:00Z",
    lastRunDate: "2026-06-18T22:03:00Z",
    failureCount: 3,
    successRate: 98.7,
    status: "Active"
  },
  {
    flowId: "f0c5b3a4-884c-4e52-b721-000000000202",
    flowName: "High risk vendor escalation",
    environmentId: environments[0].environmentId,
    ownerId: "u-1003",
    ownerName: "Priya Raman",
    createdDate: "2025-05-24T04:00:00Z",
    modifiedDate: "2026-06-15T04:50:00Z",
    lastRunDate: "2026-06-18T13:10:00Z",
    failureCount: 12,
    successRate: 91.4,
    status: "Active"
  },
  {
    flowId: "f0c5b3a4-884c-4e52-b721-000000000203",
    flowName: "Monthly ownership attestation",
    environmentId: environments[0].environmentId,
    ownerId: "u-1006",
    ownerName: "Olivia Wright",
    createdDate: "2026-01-06T23:40:00Z",
    modifiedDate: "2026-06-01T09:15:00Z",
    lastRunDate: "2026-06-01T10:00:00Z",
    failureCount: 0,
    successRate: 100,
    status: "Active"
  },
  {
    flowId: "f0c5b3a4-884c-4e52-b721-000000000204",
    flowName: "Default environment cleanup notice",
    environmentId: environments[3].environmentId,
    ownerId: "",
    ownerName: "Disabled owner",
    createdDate: "2024-09-02T00:25:00Z",
    modifiedDate: "2025-11-18T01:30:00Z",
    lastRunDate: "2026-02-04T02:10:00Z",
    failureCount: 27,
    successRate: 72.8,
    status: "Suspended"
  },
  {
    flowId: "f0c5b3a4-884c-4e52-b721-000000000205",
    flowName: "Finance approval reminder",
    environmentId: environments[0].environmentId,
    ownerId: "u-1005",
    ownerName: "Grace Tan",
    createdDate: "2025-01-12T06:00:00Z",
    modifiedDate: "2026-06-17T01:30:00Z",
    lastRunDate: "2026-06-18T21:00:00Z",
    failureCount: 5,
    successRate: 97.2,
    status: "Active"
  }
];

export const governanceRecords: GovernanceRecord[] = [
  {
    governanceId: "g-3001",
    assetType: "Application",
    assetId: applications[0].appId,
    businessOwner: "Regional Operations",
    supportTeam: "Field Operations Platform",
    criticality: "Business Critical",
    lifecycleStatus: "Active",
    reviewDate: "2026-07-15T00:00:00Z",
    comments: "Critical field workflow with validated support assignment."
  },
  {
    governanceId: "g-3002",
    assetType: "Application",
    assetId: applications[3].appId,
    businessOwner: "Facilities",
    supportTeam: "Facilities Operations",
    criticality: "Medium",
    lifecycleStatus: "Review Due",
    reviewDate: "2026-06-24T00:00:00Z",
    comments: "Owner needs reassignment before next review cycle."
  }
];

export const riskRecords: RiskRecord[] = [
  {
    riskId: "r-4001",
    assetType: "Application",
    assetId: applications[3].appId,
    riskLevel: "Critical",
    riskCategory: "Ownership",
    description: "Application has no accountable owner and has not been reviewed this quarter.",
    reviewDate: "2026-06-24T00:00:00Z"
  },
  {
    riskId: "r-4002",
    assetType: "Flow",
    assetId: flows[3].flowId,
    riskLevel: "High",
    riskCategory: "Operational",
    description: "Suspended cleanup automation has repeated failures in the default environment.",
    reviewDate: "2026-06-26T00:00:00Z"
  },
  {
    riskId: "r-4003",
    assetType: "Application",
    assetId: applications[2].appId,
    riskLevel: "High",
    riskCategory: "Compliance",
    description: "Vendor onboarding app requires DLP and owner attestation follow-up.",
    reviewDate: "2026-07-03T00:00:00Z"
  }
];

const recentActivity: ActivityItem[] = [
  {
    id: "a-5001",
    title: "Ownership review due",
    detail: "Facilities Incident Capture needs owner reassignment before the June governance checkpoint.",
    severity: "Warning",
    occurredAt: "2026-06-18T23:20:00Z"
  },
  {
    id: "a-5002",
    title: "Flow failures increased",
    detail: "High risk vendor escalation recorded 12 failures over the current review window.",
    severity: "Critical",
    occurredAt: "2026-06-18T13:10:00Z"
  },
  {
    id: "a-5003",
    title: "Governance coverage improved",
    detail: "Monthly ownership attestation completed for production finance assets.",
    severity: "Success",
    occurredAt: "2026-06-17T22:30:00Z"
  }
];

export const dashboardSummary: DashboardSummary = {
  metrics: {
    totalApplications: 415,
    totalFlows: 645,
    totalEnvironments: environments.length,
    totalMakers: 302,
    totalConnectors: 58,
    appsWithoutOwners: 18,
    disabledOwnerAssets: 24,
    inactiveAssets: 67,
    failedFlowsLastSevenDays: 41
  },
  health: {
    platformHealth: "Degraded",
    ownershipCoveragePercent: 94,
    governanceCompliancePercent: 87,
    reviewCompletionPercent: 76,
    capacityUtilisationPercent: 68
  },
  trends: [
    { period: "Jan", applications: 338, flows: 492, failures: 52, reviews: 61 },
    { period: "Feb", applications: 351, flows: 516, failures: 49, reviews: 68 },
    { period: "Mar", applications: 366, flows: 552, failures: 46, reviews: 72 },
    { period: "Apr", applications: 382, flows: 579, failures: 43, reviews: 75 },
    { period: "May", applications: 401, flows: 612, failures: 39, reviews: 78 },
    { period: "Jun", applications: 415, flows: 645, failures: 41, reviews: 82 }
  ],
  environmentDistribution: [
    { name: "Production", value: 281 },
    { name: "Sandbox", value: 63 },
    { name: "Default", value: 71 }
  ],
  riskDistribution: [
    { name: "Low", value: 246 },
    { name: "Medium", value: 108 },
    { name: "High", value: 48 },
    { name: "Critical", value: 13 }
  ],
  recentActivity
};
