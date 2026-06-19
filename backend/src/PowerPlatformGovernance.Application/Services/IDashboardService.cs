using PowerPlatformGovernance.Application.Models;

namespace PowerPlatformGovernance.Application.Services;

public interface IDashboardService
{
    Task<DashboardSummary> GetSummaryAsync(CancellationToken cancellationToken = default);
}
