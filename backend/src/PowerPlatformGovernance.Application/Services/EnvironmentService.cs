using PowerPlatformGovernance.Application.Common;
using PowerPlatformGovernance.Application.Queries;
using PowerPlatformGovernance.Domain.Repositories;
using PlatformEnvironment = PowerPlatformGovernance.Domain.Entities.Environment;

namespace PowerPlatformGovernance.Application.Services;

public sealed class EnvironmentService(IEnvironmentRepository environmentRepository) : IEnvironmentService
{
    public Task<IReadOnlyCollection<PlatformEnvironment>> GetEnvironmentsAsync(
        CancellationToken cancellationToken = default)
    {
        return environmentRepository.GetAllAsync(cancellationToken);
    }

    public async Task<PagedResult<PlatformEnvironment>> GetEnvironmentsAsync(
        EnvironmentInventoryQuery query,
        CancellationToken cancellationToken = default)
    {
        var environments = await GetFilteredEnvironmentsAsync(query, cancellationToken);

        return PagedResult<PlatformEnvironment>.Create(environments, query.Page, query.PageSize);
    }

    public Task<PlatformEnvironment?> GetEnvironmentByIdAsync(
        Guid environmentId,
        CancellationToken cancellationToken = default)
    {
        return environmentRepository.GetByIdAsync(environmentId, cancellationToken);
    }

    public Task<IReadOnlyCollection<PlatformEnvironment>> GetEnvironmentsForExportAsync(
        EnvironmentInventoryQuery query,
        CancellationToken cancellationToken = default)
    {
        return GetFilteredEnvironmentsAsync(query, cancellationToken);
    }

    private async Task<IReadOnlyCollection<PlatformEnvironment>> GetFilteredEnvironmentsAsync(
        EnvironmentInventoryQuery query,
        CancellationToken cancellationToken)
    {
        var environments = await environmentRepository.GetAllAsync(cancellationToken);
        var filteredEnvironments = environments.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            filteredEnvironments = filteredEnvironments.Where(environment =>
                environment.EnvironmentName.Contains(query.Search, StringComparison.OrdinalIgnoreCase)
                || environment.EnvironmentType.Contains(query.Search, StringComparison.OrdinalIgnoreCase)
                || environment.Region.Contains(query.Search, StringComparison.OrdinalIgnoreCase)
                || environment.DlpPolicyName.Contains(query.Search, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.EnvironmentType))
        {
            filteredEnvironments = filteredEnvironments.Where(environment =>
                string.Equals(environment.EnvironmentType, query.EnvironmentType, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Region))
        {
            filteredEnvironments = filteredEnvironments.Where(environment =>
                string.Equals(environment.Region, query.Region, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.DlpPolicyName))
        {
            filteredEnvironments = filteredEnvironments.Where(environment =>
                string.Equals(environment.DlpPolicyName, query.DlpPolicyName, StringComparison.OrdinalIgnoreCase));
        }

        return Sort(filteredEnvironments, query.SortBy, query.SortDirection).ToArray();
    }

    private static IEnumerable<PlatformEnvironment> Sort(
        IEnumerable<PlatformEnvironment> environments,
        string? sortBy,
        SortDirection sortDirection)
    {
        var sortedEnvironments = (sortBy ?? string.Empty).ToLowerInvariant() switch
        {
            "environmentname" or "name" => environments.OrderBy(environment => environment.EnvironmentName),
            "environmenttype" or "type" => environments.OrderBy(environment => environment.EnvironmentType),
            "region" => environments.OrderBy(environment => environment.Region),
            "capacityusedmb" or "capacityused" => environments.OrderBy(environment => environment.CapacityUsedMb),
            "capacityallocatedmb" or "capacityallocated" => environments.OrderBy(environment => environment.CapacityAllocatedMb),
            "appcount" => environments.OrderBy(environment => environment.AppCount),
            "flowcount" => environments.OrderBy(environment => environment.FlowCount),
            "makercount" => environments.OrderBy(environment => environment.MakerCount),
            "dlppolicyname" or "dlppolicy" => environments.OrderBy(environment => environment.DlpPolicyName),
            _ => environments.OrderBy(environment => environment.EnvironmentName)
        };

        return sortDirection == SortDirection.Descending
            ? sortedEnvironments.Reverse()
            : sortedEnvironments;
    }
}
