using PowerPlatformGovernance.Domain.Repositories;
using PlatformApplication = PowerPlatformGovernance.Domain.Entities.Application;

namespace PowerPlatformGovernance.Application.Services;

public sealed class ApplicationService(IApplicationRepository applicationRepository) : IApplicationService
{
    public Task<IReadOnlyCollection<PlatformApplication>> GetApplicationsAsync(
        CancellationToken cancellationToken = default)
    {
        return applicationRepository.GetAllAsync(cancellationToken);
    }

    public Task<PlatformApplication?> GetApplicationByIdAsync(
        Guid appId,
        CancellationToken cancellationToken = default)
    {
        return applicationRepository.GetByIdAsync(appId, cancellationToken);
    }
}
