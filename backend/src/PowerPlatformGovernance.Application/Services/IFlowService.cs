using PowerPlatformGovernance.Domain.Entities;

namespace PowerPlatformGovernance.Application.Services;

public interface IFlowService
{
    Task<IReadOnlyCollection<Flow>> GetFlowsAsync(CancellationToken cancellationToken = default);

    Task<Flow?> GetFlowByIdAsync(Guid flowId, CancellationToken cancellationToken = default);
}
