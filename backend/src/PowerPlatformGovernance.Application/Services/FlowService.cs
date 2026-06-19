using PowerPlatformGovernance.Application.Common;
using PowerPlatformGovernance.Application.Queries;
using PowerPlatformGovernance.Domain.Entities;
using PowerPlatformGovernance.Domain.Repositories;

namespace PowerPlatformGovernance.Application.Services;

public sealed class FlowService(IFlowRepository flowRepository) : IFlowService
{
    public Task<IReadOnlyCollection<Flow>> GetFlowsAsync(CancellationToken cancellationToken = default)
    {
        return flowRepository.GetAllAsync(cancellationToken);
    }

    public async Task<PagedResult<Flow>> GetFlowsAsync(
        FlowInventoryQuery query,
        CancellationToken cancellationToken = default)
    {
        var flows = await GetFilteredFlowsAsync(query, cancellationToken);

        return PagedResult<Flow>.Create(flows, query.Page, query.PageSize);
    }

    public Task<Flow?> GetFlowByIdAsync(Guid flowId, CancellationToken cancellationToken = default)
    {
        return flowRepository.GetByIdAsync(flowId, cancellationToken);
    }

    public Task<IReadOnlyCollection<Flow>> GetFlowsForExportAsync(
        FlowInventoryQuery query,
        CancellationToken cancellationToken = default)
    {
        return GetFilteredFlowsAsync(query, cancellationToken);
    }

    private async Task<IReadOnlyCollection<Flow>> GetFilteredFlowsAsync(
        FlowInventoryQuery query,
        CancellationToken cancellationToken)
    {
        var flows = await flowRepository.GetAllAsync(cancellationToken);
        var filteredFlows = flows.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            filteredFlows = filteredFlows.Where(flow =>
                flow.FlowName.Contains(query.Search, StringComparison.OrdinalIgnoreCase)
                || flow.OwnerName.Contains(query.Search, StringComparison.OrdinalIgnoreCase));
        }

        if (query.EnvironmentId.HasValue)
        {
            filteredFlows = filteredFlows.Where(flow => flow.EnvironmentId == query.EnvironmentId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.OwnerId))
        {
            filteredFlows = filteredFlows.Where(
                flow => string.Equals(flow.OwnerId, query.OwnerId, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Status))
        {
            filteredFlows = filteredFlows.Where(
                flow => string.Equals(flow.Status, query.Status, StringComparison.OrdinalIgnoreCase));
        }

        if (query.MinimumFailureCount.HasValue)
        {
            filteredFlows = filteredFlows.Where(flow => flow.FailureCount >= query.MinimumFailureCount.Value);
        }

        return Sort(filteredFlows, query.SortBy, query.SortDirection).ToArray();
    }

    private static IEnumerable<Flow> Sort(
        IEnumerable<Flow> flows,
        string? sortBy,
        SortDirection sortDirection)
    {
        var sortedFlows = (sortBy ?? string.Empty).ToLowerInvariant() switch
        {
            "flowname" or "name" => flows.OrderBy(flow => flow.FlowName),
            "owner" or "ownername" => flows.OrderBy(flow => flow.OwnerName),
            "createddate" => flows.OrderBy(flow => flow.CreatedDate),
            "modifieddate" or "lastmodified" => flows.OrderBy(flow => flow.ModifiedDate),
            "lastrundate" or "lastrun" => flows.OrderBy(flow => flow.LastRunDate),
            "failurecount" => flows.OrderBy(flow => flow.FailureCount),
            "successrate" => flows.OrderBy(flow => flow.SuccessRate),
            "status" => flows.OrderBy(flow => flow.Status),
            _ => flows.OrderBy(flow => flow.FlowName)
        };

        return sortDirection == SortDirection.Descending
            ? sortedFlows.Reverse()
            : sortedFlows;
    }
}
