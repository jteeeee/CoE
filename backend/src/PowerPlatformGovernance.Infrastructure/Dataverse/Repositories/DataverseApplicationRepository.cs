using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using PowerPlatformGovernance.Domain.Repositories;
using PowerPlatformGovernance.Infrastructure.Dataverse;
using PlatformApplication = PowerPlatformGovernance.Domain.Entities.Application;

namespace PowerPlatformGovernance.Infrastructure.Dataverse.Repositories;

public sealed class DataverseApplicationRepository(
    IDataverseClient dataverseClient,
    IOptions<DataverseOptions> options) : IApplicationRepository
{
    private readonly ApplicationTableMapping mapping = options.Value.Tables.Applications;

    public async Task<IReadOnlyCollection<PlatformApplication>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var entities = await dataverseClient.RetrieveAllAsync(CreateBaseQuery(), cancellationToken);

        return entities.Select(MapToDomain).ToArray();
    }

    public async Task<PlatformApplication?> GetByIdAsync(
        Guid appId,
        CancellationToken cancellationToken = default)
    {
        var query = CreateBaseQuery();
        query.Criteria.AddCondition(DataverseEntityReader.BuildIdCondition(mapping.AppId, appId));
        query.TopCount = 1;

        var entities = await dataverseClient.RetrieveAllAsync(query, cancellationToken);

        return entities.Select(MapToDomain).FirstOrDefault();
    }

    private QueryExpression CreateBaseQuery()
    {
        return new QueryExpression(mapping.TableName)
        {
            ColumnSet = DataverseEntityReader.BuildColumnSet(
                mapping.AppId,
                mapping.AppName,
                mapping.EnvironmentId,
                mapping.EnvironmentName,
                mapping.OwnerId,
                mapping.OwnerName,
                mapping.OwnerEmail,
                mapping.CreatedDate,
                mapping.ModifiedDate,
                mapping.LastUsedDate,
                mapping.UserCount,
                mapping.Status,
                mapping.RiskRating,
                mapping.BusinessCriticality,
                mapping.SupportTeam),
            Criteria = new FilterExpression(LogicalOperator.And)
        };
    }

    private PlatformApplication MapToDomain(Entity entity)
    {
        return new PlatformApplication
        {
            AppId = DataverseEntityReader.GetGuid(entity, mapping.AppId),
            AppName = DataverseEntityReader.GetString(entity, mapping.AppName),
            EnvironmentId = DataverseEntityReader.GetGuid(entity, mapping.EnvironmentId),
            EnvironmentName = DataverseEntityReader.GetString(entity, mapping.EnvironmentName),
            OwnerId = DataverseEntityReader.GetString(entity, mapping.OwnerId),
            OwnerName = DataverseEntityReader.GetString(entity, mapping.OwnerName),
            OwnerEmail = DataverseEntityReader.GetString(entity, mapping.OwnerEmail),
            CreatedDate = DataverseEntityReader.GetDateTime(entity, mapping.CreatedDate),
            ModifiedDate = DataverseEntityReader.GetDateTime(entity, mapping.ModifiedDate),
            LastUsedDate = DataverseEntityReader.GetDateTime(entity, mapping.LastUsedDate),
            UserCount = DataverseEntityReader.GetInt(entity, mapping.UserCount),
            Status = DataverseEntityReader.GetString(entity, mapping.Status),
            RiskRating = DataverseEntityReader.GetString(entity, mapping.RiskRating),
            BusinessCriticality = DataverseEntityReader.GetString(entity, mapping.BusinessCriticality),
            SupportTeam = DataverseEntityReader.GetString(entity, mapping.SupportTeam)
        };
    }
}
