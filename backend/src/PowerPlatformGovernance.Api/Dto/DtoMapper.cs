using PowerPlatformGovernance.Application.Common;
using PowerPlatformGovernance.Application.Models;
using PowerPlatformGovernance.Domain.Entities;
using PlatformApplication = PowerPlatformGovernance.Domain.Entities.Application;
using PlatformEnvironment = PowerPlatformGovernance.Domain.Entities.Environment;

namespace PowerPlatformGovernance.Api.Dto;

internal static class DtoMapper
{
    public static PagedResultDto<TDestination> ToDto<TSource, TDestination>(
        PagedResult<TSource> result,
        Func<TSource, TDestination> map)
    {
        return new PagedResultDto<TDestination>(
            result.Items.Select(map).ToArray(),
            result.Page,
            result.PageSize,
            result.TotalCount,
            result.TotalPages,
            result.HasPreviousPage,
            result.HasNextPage);
    }

    public static DashboardDto ToDto(DashboardSummary summary)
    {
        return new DashboardDto(
            summary.TotalApplications,
            summary.ActiveApplications,
            summary.InactiveApplications,
            summary.OrphanedApplications,
            summary.HighRiskApplications,
            summary.TotalFlows,
            summary.FailedFlows,
            summary.InactiveFlows,
            summary.OrphanedFlows,
            summary.TotalEnvironments,
            summary.ProductionEnvironments,
            summary.SandboxEnvironments,
            summary.CapacityUtilisationPercentage,
            summary.TotalMakers,
            summary.DisabledOwnerAssets,
            summary.AssetsNotUsedIn90Days,
            summary.FailedFlowsLast7Days);
    }

    public static ApplicationDto ToDto(PlatformApplication application)
    {
        return new ApplicationDto(
            application.AppId,
            application.AppName,
            application.EnvironmentId,
            application.EnvironmentName,
            application.OwnerId,
            application.OwnerName,
            application.OwnerEmail,
            application.CreatedDate,
            application.ModifiedDate,
            application.LastUsedDate,
            application.UserCount,
            application.Status,
            application.RiskRating,
            application.BusinessCriticality,
            application.SupportTeam);
    }

    public static FlowDto ToDto(Flow flow)
    {
        return new FlowDto(
            flow.FlowId,
            flow.FlowName,
            flow.EnvironmentId,
            flow.OwnerId,
            flow.OwnerName,
            flow.CreatedDate,
            flow.ModifiedDate,
            flow.LastRunDate,
            flow.FailureCount,
            flow.SuccessRate,
            flow.Status);
    }

    public static EnvironmentDto ToDto(PlatformEnvironment environment)
    {
        return new EnvironmentDto(
            environment.EnvironmentId,
            environment.EnvironmentName,
            environment.EnvironmentType,
            environment.Region,
            environment.CapacityUsedMb,
            environment.CapacityAllocatedMb,
            environment.AppCount,
            environment.FlowCount,
            environment.MakerCount,
            environment.DlpPolicyName);
    }
}
