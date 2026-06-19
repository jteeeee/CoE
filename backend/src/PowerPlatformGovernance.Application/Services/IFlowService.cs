using PowerPlatformGovernance.Application.Common;
using PowerPlatformGovernance.Application.Queries;
using PowerPlatformGovernance.Domain.Entities;

namespace PowerPlatformGovernance.Application.Services;

public interface IFlowService
{
    Task<IReadOnlyCollection<Flow>> GetFlowsAsync(CancellationToken cancellationToken = default);

    Task<PagedResult<Flow>> GetFlowsAsync(
        FlowInventoryQuery query,
        CancellationToken cancellationToken = default);

    Task<Flow?> GetFlowByIdAsync(Guid flowId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Flow>> GetFlowsForExportAsync(
        FlowInventoryQuery query,
        CancellationToken cancellationToken = default);
}
