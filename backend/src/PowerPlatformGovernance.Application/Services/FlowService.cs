using PowerPlatformGovernance.Domain.Entities;
using PowerPlatformGovernance.Domain.Repositories;

namespace PowerPlatformGovernance.Application.Services;

public sealed class FlowService(IFlowRepository flowRepository) : IFlowService
{
    public Task<IReadOnlyCollection<Flow>> GetFlowsAsync(CancellationToken cancellationToken = default)
    {
        return flowRepository.GetAllAsync(cancellationToken);
    }

    public Task<Flow?> GetFlowByIdAsync(Guid flowId, CancellationToken cancellationToken = default)
    {
        return flowRepository.GetByIdAsync(flowId, cancellationToken);
    }
}
