using PowerPlatformGovernance.Application.Common;
using PowerPlatformGovernance.Application.Queries;
using PlatformApplication = PowerPlatformGovernance.Domain.Entities.Application;

namespace PowerPlatformGovernance.Application.Services;

public interface IApplicationService
{
    Task<IReadOnlyCollection<PlatformApplication>> GetApplicationsAsync(
        CancellationToken cancellationToken = default);

    Task<PagedResult<PlatformApplication>> GetApplicationsAsync(
        ApplicationInventoryQuery query,
        CancellationToken cancellationToken = default);

    Task<PlatformApplication?> GetApplicationByIdAsync(
        Guid appId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PlatformApplication>> SearchApplicationsAsync(
        string? query,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PlatformApplication>> GetApplicationsForExportAsync(
        ApplicationInventoryQuery query,
        CancellationToken cancellationToken = default);
}
