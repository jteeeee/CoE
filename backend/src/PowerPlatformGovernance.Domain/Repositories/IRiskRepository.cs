using PowerPlatformGovernance.Domain.Entities;

namespace PowerPlatformGovernance.Domain.Repositories;

public interface IRiskRepository
{
    Task<IReadOnlyCollection<RiskRecord>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<RiskRecord?> GetByIdAsync(Guid riskId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<RiskRecord>> GetByAssetAsync(
        string assetType,
        Guid assetId,
        CancellationToken cancellationToken = default);
}
