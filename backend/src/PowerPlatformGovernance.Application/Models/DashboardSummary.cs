namespace PowerPlatformGovernance.Application.Models;

public sealed record DashboardSummary(
    int TotalApplications,
    int ActiveApplications,
    int InactiveApplications,
    int OrphanedApplications,
    int HighRiskApplications,
    int TotalFlows,
    int FailedFlows,
    int InactiveFlows,
    int OrphanedFlows,
    int TotalEnvironments,
    int ProductionEnvironments,
    int SandboxEnvironments,
    decimal CapacityUtilisationPercentage,
    int TotalMakers,
    int DisabledOwnerAssets,
    int AssetsNotUsedIn90Days,
    int FailedFlowsLast7Days);
