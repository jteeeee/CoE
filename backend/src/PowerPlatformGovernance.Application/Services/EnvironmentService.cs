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

    public Task<PlatformEnvironment?> GetEnvironmentByIdAsync(
        Guid environmentId,
        CancellationToken cancellationToken = default)
    {
        return environmentRepository.GetByIdAsync(environmentId, cancellationToken);
    }
}
