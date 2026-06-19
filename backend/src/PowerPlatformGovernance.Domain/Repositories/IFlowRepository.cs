using PowerPlatformGovernance.Domain.Entities;

namespace PowerPlatformGovernance.Domain.Repositories;

public interface IFlowRepository
{
    Task<IReadOnlyCollection<Flow>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Flow?> GetByIdAsync(Guid flowId, CancellationToken cancellationToken = default);
}
