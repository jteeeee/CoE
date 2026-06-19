using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using PowerPlatformGovernance.Domain.Repositories;
using PlatformEnvironment = PowerPlatformGovernance.Domain.Entities.Environment;

namespace PowerPlatformGovernance.Infrastructure.Dataverse.Repositories;

public sealed class DataverseEnvironmentRepository(
    IDataverseClient dataverseClient,
    IOptions<DataverseOptions> options) : IEnvironmentRepository
{
    private readonly EnvironmentTableMapping mapping = options.Value.Tables.Environments;

    public async Task<IReadOnlyCollection<PlatformEnvironment>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var entities = await dataverseClient.RetrieveAllAsync(CreateBaseQuery(), cancellationToken);

        return entities.Select(MapToDomain).ToArray();
    }

    public async Task<PlatformEnvironment?> GetByIdAsync(
        Guid environmentId,
        CancellationToken cancellationToken = default)
    {
        var query = CreateBaseQuery();
        query.Criteria.AddCondition(DataverseEntityReader.BuildIdCondition(mapping.EnvironmentId, environmentId));
        query.TopCount = 1;

        var entities = await dataverseClient.RetrieveAllAsync(query, cancellationToken);

        return entities.Select(MapToDomain).FirstOrDefault();
    }

    private QueryExpression CreateBaseQuery()
    {
        return new QueryExpression(mapping.TableName)
        {
            ColumnSet = DataverseEntityReader.BuildColumnSet(
                mapping.EnvironmentId,
                mapping.EnvironmentName,
                mapping.EnvironmentType,
                mapping.Region,
                mapping.CapacityUsedMb,
                mapping.CapacityAllocatedMb,
                mapping.AppCount,
                mapping.FlowCount,
                mapping.MakerCount,
                mapping.DlpPolicyName),
            Criteria = new FilterExpression(LogicalOperator.And)
        };
    }

    private PlatformEnvironment MapToDomain(Entity entity)
    {
        return new PlatformEnvironment
        {
            EnvironmentId = DataverseEntityReader.GetGuid(entity, mapping.EnvironmentId),
            EnvironmentName = DataverseEntityReader.GetString(entity, mapping.EnvironmentName),
            EnvironmentType = DataverseEntityReader.GetString(entity, mapping.EnvironmentType),
            Region = DataverseEntityReader.GetString(entity, mapping.Region),
            CapacityUsedMb = DataverseEntityReader.GetDecimal(entity, mapping.CapacityUsedMb),
            CapacityAllocatedMb = DataverseEntityReader.GetDecimal(entity, mapping.CapacityAllocatedMb),
            AppCount = DataverseEntityReader.GetInt(entity, mapping.AppCount),
            FlowCount = DataverseEntityReader.GetInt(entity, mapping.FlowCount),
            MakerCount = DataverseEntityReader.GetInt(entity, mapping.MakerCount),
            DlpPolicyName = DataverseEntityReader.GetString(entity, mapping.DlpPolicyName)
        };
    }
}
