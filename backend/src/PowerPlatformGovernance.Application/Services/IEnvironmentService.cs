using PowerPlatformGovernance.Application.Common;
using PowerPlatformGovernance.Application.Queries;
using PlatformEnvironment = PowerPlatformGovernance.Domain.Entities.Environment;

namespace PowerPlatformGovernance.Application.Services;

public interface IEnvironmentService
{
    Task<IReadOnlyCollection<PlatformEnvironment>> GetEnvironmentsAsync(
        CancellationToken cancellationToken = default);

    Task<PagedResult<PlatformEnvironment>> GetEnvironmentsAsync(
        EnvironmentInventoryQuery query,
        CancellationToken cancellationToken = default);

    Task<PlatformEnvironment?> GetEnvironmentByIdAsync(
        Guid environmentId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PlatformEnvironment>> GetEnvironmentsForExportAsync(
        EnvironmentInventoryQuery query,
        CancellationToken cancellationToken = default);
}
