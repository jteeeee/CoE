using PlatformEnvironment = PowerPlatformGovernance.Domain.Entities.Environment;

namespace PowerPlatformGovernance.Application.Services;

public interface IEnvironmentService
{
    Task<IReadOnlyCollection<PlatformEnvironment>> GetEnvironmentsAsync(
        CancellationToken cancellationToken = default);

    Task<PlatformEnvironment?> GetEnvironmentByIdAsync(
        Guid environmentId,
        CancellationToken cancellationToken = default);
}
