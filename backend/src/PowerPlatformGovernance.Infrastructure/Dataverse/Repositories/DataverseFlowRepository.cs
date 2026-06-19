using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using PowerPlatformGovernance.Domain.Entities;
using PowerPlatformGovernance.Domain.Repositories;

namespace PowerPlatformGovernance.Infrastructure.Dataverse.Repositories;

public sealed class DataverseFlowRepository(
    IDataverseClient dataverseClient,
    IOptions<DataverseOptions> options) : IFlowRepository
{
    private readonly FlowTableMapping mapping = options.Value.Tables.Flows;

    public async Task<IReadOnlyCollection<Flow>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await dataverseClient.RetrieveAllAsync(CreateBaseQuery(), cancellationToken);

        return entities.Select(MapToDomain).ToArray();
    }

    public async Task<Flow?> GetByIdAsync(Guid flowId, CancellationToken cancellationToken = default)
    {
        var query = CreateBaseQuery();
        query.Criteria.AddCondition(DataverseEntityReader.BuildIdCondition(mapping.FlowId, flowId));
        query.TopCount = 1;

        var entities = await dataverseClient.RetrieveAllAsync(query, cancellationToken);

        return entities.Select(MapToDomain).FirstOrDefault();
    }

    private QueryExpression CreateBaseQuery()
    {
        return new QueryExpression(mapping.TableName)
        {
            ColumnSet = DataverseEntityReader.BuildColumnSet(
                mapping.FlowId,
                mapping.FlowName,
                mapping.EnvironmentId,
                mapping.OwnerId,
                mapping.OwnerName,
                mapping.CreatedDate,
                mapping.ModifiedDate,
                mapping.LastRunDate,
                mapping.FailureCount,
                mapping.SuccessRate,
                mapping.Status),
            Criteria = new FilterExpression(LogicalOperator.And)
        };
    }

    private Flow MapToDomain(Entity entity)
    {
        return new Flow
        {
            FlowId = DataverseEntityReader.GetGuid(entity, mapping.FlowId),
            FlowName = DataverseEntityReader.GetString(entity, mapping.FlowName),
            EnvironmentId = DataverseEntityReader.GetGuid(entity, mapping.EnvironmentId),
            OwnerId = DataverseEntityReader.GetString(entity, mapping.OwnerId),
            OwnerName = DataverseEntityReader.GetString(entity, mapping.OwnerName),
            CreatedDate = DataverseEntityReader.GetDateTime(entity, mapping.CreatedDate),
            ModifiedDate = DataverseEntityReader.GetDateTime(entity, mapping.ModifiedDate),
            LastRunDate = DataverseEntityReader.GetDateTime(entity, mapping.LastRunDate),
            FailureCount = DataverseEntityReader.GetInt(entity, mapping.FailureCount),
            SuccessRate = DataverseEntityReader.GetDecimal(entity, mapping.SuccessRate),
            Status = DataverseEntityReader.GetString(entity, mapping.Status)
        };
    }
}
