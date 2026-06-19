using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerPlatformGovernance.Domain.Repositories;
using PowerPlatformGovernance.Infrastructure.Dataverse;
using PowerPlatformGovernance.Infrastructure.Dataverse.Repositories;

namespace PowerPlatformGovernance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DataverseOptions>(configuration.GetSection(DataverseOptions.SectionName));

        services.AddScoped<IDataverseClient, DataverseClient>();
        services.AddScoped<IApplicationRepository, DataverseApplicationRepository>();
        services.AddScoped<IFlowRepository, DataverseFlowRepository>();
        services.AddScoped<IEnvironmentRepository, DataverseEnvironmentRepository>();

        return services;
    }
}
