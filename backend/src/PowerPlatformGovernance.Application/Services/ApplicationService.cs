using PowerPlatformGovernance.Application.Common;
using PowerPlatformGovernance.Application.Queries;
using PowerPlatformGovernance.Domain.Repositories;
using PlatformApplication = PowerPlatformGovernance.Domain.Entities.Application;

namespace PowerPlatformGovernance.Application.Services;

public sealed class ApplicationService(IApplicationRepository applicationRepository) : IApplicationService
{
    public Task<IReadOnlyCollection<PlatformApplication>> GetApplicationsAsync(
        CancellationToken cancellationToken = default)
    {
        return applicationRepository.GetAllAsync(cancellationToken);
    }

    public async Task<PagedResult<PlatformApplication>> GetApplicationsAsync(
        ApplicationInventoryQuery query,
        CancellationToken cancellationToken = default)
    {
        var applications = await GetFilteredApplicationsAsync(query, cancellationToken);

        return PagedResult<PlatformApplication>.Create(applications, query.Page, query.PageSize);
    }

    public Task<PlatformApplication?> GetApplicationByIdAsync(
        Guid appId,
        CancellationToken cancellationToken = default)
    {
        return applicationRepository.GetByIdAsync(appId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<PlatformApplication>> SearchApplicationsAsync(
        string? query,
        CancellationToken cancellationToken = default)
    {
        return await GetApplicationsForExportAsync(
            new ApplicationInventoryQuery { Search = query },
            cancellationToken);
    }

    public Task<IReadOnlyCollection<PlatformApplication>> GetApplicationsForExportAsync(
        ApplicationInventoryQuery query,
        CancellationToken cancellationToken = default)
    {
        return GetFilteredApplicationsAsync(query, cancellationToken);
    }

    private async Task<IReadOnlyCollection<PlatformApplication>> GetFilteredApplicationsAsync(
        ApplicationInventoryQuery query,
        CancellationToken cancellationToken)
    {
        var applications = await applicationRepository.GetAllAsync(cancellationToken);
        var filteredApplications = applications.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            filteredApplications = filteredApplications.Where(application =>
                application.AppName.Contains(query.Search, StringComparison.OrdinalIgnoreCase)
                || application.EnvironmentName.Contains(query.Search, StringComparison.OrdinalIgnoreCase)
                || application.OwnerName.Contains(query.Search, StringComparison.OrdinalIgnoreCase)
                || application.OwnerEmail.Contains(query.Search, StringComparison.OrdinalIgnoreCase));
        }

        if (query.EnvironmentId.HasValue)
        {
            filteredApplications = filteredApplications.Where(
                application => application.EnvironmentId == query.EnvironmentId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.OwnerId))
        {
            filteredApplications = filteredApplications.Where(
                application => string.Equals(application.OwnerId, query.OwnerId, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Status))
        {
            filteredApplications = filteredApplications.Where(
                application => string.Equals(application.Status, query.Status, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.RiskRating))
        {
            filteredApplications = filteredApplications.Where(
                application => string.Equals(application.RiskRating, query.RiskRating, StringComparison.OrdinalIgnoreCase));
        }

        return Sort(filteredApplications, query.SortBy, query.SortDirection).ToArray();
    }

    private static IEnumerable<PlatformApplication> Sort(
        IEnumerable<PlatformApplication> applications,
        string? sortBy,
        SortDirection sortDirection)
    {
        var sortedApplications = (sortBy ?? string.Empty).ToLowerInvariant() switch
        {
            "appname" or "name" => applications.OrderBy(application => application.AppName),
            "environment" or "environmentname" => applications.OrderBy(application => application.EnvironmentName),
            "owner" or "ownername" => applications.OrderBy(application => application.OwnerName),
            "createddate" => applications.OrderBy(application => application.CreatedDate),
            "modifieddate" or "lastmodified" => applications.OrderBy(application => application.ModifiedDate),
            "lastuseddate" or "lastused" => applications.OrderBy(application => application.LastUsedDate),
            "usercount" => applications.OrderBy(application => application.UserCount),
            "status" => applications.OrderBy(application => application.Status),
            "riskrating" => applications.OrderBy(application => application.RiskRating),
            "businesscriticality" => applications.OrderBy(application => application.BusinessCriticality),
            "supportteam" => applications.OrderBy(application => application.SupportTeam),
            _ => applications.OrderBy(application => application.AppName)
        };

        return sortDirection == SortDirection.Descending
            ? sortedApplications.Reverse()
            : sortedApplications;
    }
}
