using Microsoft.Extensions.DependencyInjection;
using PowerPlatformGovernance.Application.Services;

namespace PowerPlatformGovernance.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IFlowService, FlowService>();
        services.AddScoped<IEnvironmentService, EnvironmentService>();
        services.AddScoped<IDashboardService, DashboardService>();

        return services;
    }
}
