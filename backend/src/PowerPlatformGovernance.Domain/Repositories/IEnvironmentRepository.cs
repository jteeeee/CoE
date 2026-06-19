using PlatformEnvironment = PowerPlatformGovernance.Domain.Entities.Environment;

namespace PowerPlatformGovernance.Domain.Repositories;

public interface IEnvironmentRepository
{
    Task<IReadOnlyCollection<PlatformEnvironment>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PlatformEnvironment?> GetByIdAsync(Guid environmentId, CancellationToken cancellationToken = default);
}
