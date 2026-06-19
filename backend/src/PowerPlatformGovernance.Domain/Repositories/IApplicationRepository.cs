using PowerPlatformGovernance.Domain.Entities;

namespace PowerPlatformGovernance.Domain.Repositories;

public interface IApplicationRepository
{
    Task<IReadOnlyCollection<Application>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Application?> GetByIdAsync(Guid appId, CancellationToken cancellationToken = default);
}
