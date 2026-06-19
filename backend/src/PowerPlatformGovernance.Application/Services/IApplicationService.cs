using PlatformApplication = PowerPlatformGovernance.Domain.Entities.Application;

namespace PowerPlatformGovernance.Application.Services;

public interface IApplicationService
{
    Task<IReadOnlyCollection<PlatformApplication>> GetApplicationsAsync(
        CancellationToken cancellationToken = default);

    Task<PlatformApplication?> GetApplicationByIdAsync(
        Guid appId,
        CancellationToken cancellationToken = default);
}
