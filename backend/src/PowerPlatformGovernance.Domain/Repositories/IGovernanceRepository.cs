using PowerPlatformGovernance.Domain.Entities;

namespace PowerPlatformGovernance.Domain.Repositories;

public interface IGovernanceRepository
{
    Task<IReadOnlyCollection<GovernanceRecord>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<GovernanceRecord?> GetByIdAsync(Guid governanceId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<GovernanceRecord>> GetByAssetAsync(
        string assetType,
        Guid assetId,
        CancellationToken cancellationToken = default);

    Task AddAsync(GovernanceRecord governanceRecord, CancellationToken cancellationToken = default);

    Task UpdateAsync(GovernanceRecord governanceRecord, CancellationToken cancellationToken = default);
}
