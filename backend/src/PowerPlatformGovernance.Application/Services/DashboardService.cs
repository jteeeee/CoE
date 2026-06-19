using PowerPlatformGovernance.Application.Models;
using PowerPlatformGovernance.Domain.Entities;
using PowerPlatformGovernance.Domain.Repositories;
using PlatformApplication = PowerPlatformGovernance.Domain.Entities.Application;
using PlatformEnvironment = PowerPlatformGovernance.Domain.Entities.Environment;

namespace PowerPlatformGovernance.Application.Services;

public sealed class DashboardService(
    IApplicationRepository applicationRepository,
    IFlowRepository flowRepository,
    IEnvironmentRepository environmentRepository,
    IUserRepository userRepository) : IDashboardService
{
    public async Task<DashboardSummary> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var applicationsTask = applicationRepository.GetAllAsync(cancellationToken);
        var flowsTask = flowRepository.GetAllAsync(cancellationToken);
        var environmentsTask = environmentRepository.GetAllAsync(cancellationToken);
        var usersTask = userRepository.GetAllAsync(cancellationToken);

        await Task.WhenAll(applicationsTask, flowsTask, environmentsTask, usersTask);

        var applications = await applicationsTask;
        var flows = await flowsTask;
        var environments = await environmentsTask;
        var users = await usersTask;

        var inactiveCutoff = DateTime.UtcNow.AddDays(-90);
        var failedFlowCutoff = DateTime.UtcNow.AddDays(-7);
        var disabledOwnerIds = users
            .Where(IsDisabled)
            .Select(user => user.UserId)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var disabledApplicationOwners = applications.Count(
            application => disabledOwnerIds.Contains(application.OwnerId));
        var disabledFlowOwners = flows.Count(flow => disabledOwnerIds.Contains(flow.OwnerId));

        return new DashboardSummary(
            TotalApplications: applications.Count,
            ActiveApplications: applications.Count(IsActive),
            InactiveApplications: applications.Count(IsInactive),
            OrphanedApplications: applications.Count(IsOrphaned),
            HighRiskApplications: applications.Count(IsHighRisk),
            TotalFlows: flows.Count,
            FailedFlows: flows.Count(HasFailures),
            InactiveFlows: flows.Count(IsInactive),
            OrphanedFlows: flows.Count(IsOrphaned),
            TotalEnvironments: environments.Count,
            ProductionEnvironments: environments.Count(IsProduction),
            SandboxEnvironments: environments.Count(IsSandbox),
            CapacityUtilisationPercentage: CalculateCapacityUtilisationPercentage(environments),
            TotalMakers: users.Count,
            DisabledOwnerAssets: disabledApplicationOwners + disabledFlowOwners,
            AssetsNotUsedIn90Days: CountAssetsNotUsedSince(applications, flows, inactiveCutoff),
            FailedFlowsLast7Days: flows.Count(flow => HasFailures(flow) && flow.LastRunDate >= failedFlowCutoff));
    }

    private static bool IsActive(PlatformApplication application)
    {
        return IsStatus(application.Status, "Active");
    }

    private static bool IsActive(Flow flow)
    {
        return IsStatus(flow.Status, "Active");
    }

    private static bool IsInactive(PlatformApplication application)
    {
        return IsStatus(application.Status, "Inactive");
    }

    private static bool IsInactive(Flow flow)
    {
        return IsStatus(flow.Status, "Inactive");
    }

    private static bool IsOrphaned(PlatformApplication application)
    {
        return string.IsNullOrWhiteSpace(application.OwnerId)
            || string.IsNullOrWhiteSpace(application.OwnerName)
            || string.IsNullOrWhiteSpace(application.OwnerEmail);
    }

    private static bool IsOrphaned(Flow flow)
    {
        return string.IsNullOrWhiteSpace(flow.OwnerId)
            || string.IsNullOrWhiteSpace(flow.OwnerName);
    }

    private static bool IsHighRisk(PlatformApplication application)
    {
        return IsStatus(application.RiskRating, "High");
    }

    private static bool HasFailures(Flow flow)
    {
        return flow.FailureCount > 0;
    }

    private static bool IsProduction(PlatformEnvironment environment)
    {
        return IsStatus(environment.EnvironmentType, "Production");
    }

    private static bool IsSandbox(PlatformEnvironment environment)
    {
        return IsStatus(environment.EnvironmentType, "Sandbox");
    }

    private static bool IsDisabled(User user)
    {
        return IsStatus(user.Status, "Disabled");
    }

    private static bool IsStatus(string value, string expected)
    {
        return string.Equals(value, expected, StringComparison.OrdinalIgnoreCase);
    }

    private static int CountAssetsNotUsedSince(
        IReadOnlyCollection<PlatformApplication> applications,
        IReadOnlyCollection<Flow> flows,
        DateTime cutoff)
    {
        return applications.Count(application => application.LastUsedDate < cutoff)
            + flows.Count(flow => flow.LastRunDate < cutoff);
    }

    private static decimal CalculateCapacityUtilisationPercentage(
        IReadOnlyCollection<PlatformEnvironment> environments)
    {
        var totalAllocated = environments.Sum(environment => environment.CapacityAllocatedMb);

        if (totalAllocated <= 0)
        {
            return 0;
        }

        var totalUsed = environments.Sum(environment => environment.CapacityUsedMb);

        return decimal.Round(totalUsed / totalAllocated * 100, 2);
    }
}
