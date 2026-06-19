using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using PowerPlatformGovernance.Domain.Entities;
using PowerPlatformGovernance.Domain.Repositories;

namespace PowerPlatformGovernance.Infrastructure.Dataverse.Repositories;

public sealed class DataverseUserRepository(
    IDataverseClient dataverseClient,
    IOptions<DataverseOptions> options) : IUserRepository
{
    private readonly UserTableMapping mapping = options.Value.Tables.Users;

    public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await dataverseClient.RetrieveAllAsync(CreateBaseQuery(), cancellationToken);

        return entities.Select(MapToDomain).ToArray();
    }

    public async Task<User?> GetByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var query = CreateBaseQuery();
        query.Criteria.AddCondition(mapping.UserId, ConditionOperator.Equal, userId);
        query.TopCount = 1;

        var entities = await dataverseClient.RetrieveAllAsync(query, cancellationToken);

        return entities.Select(MapToDomain).FirstOrDefault();
    }

    private QueryExpression CreateBaseQuery()
    {
        return new QueryExpression(mapping.TableName)
        {
            ColumnSet = DataverseEntityReader.BuildColumnSet(
                mapping.UserId,
                mapping.DisplayName,
                mapping.EmailAddress,
                mapping.Department,
                mapping.Manager,
                mapping.Status),
            Criteria = new FilterExpression(LogicalOperator.And)
        };
    }

    private User MapToDomain(Entity entity)
    {
        return new User
        {
            UserId = DataverseEntityReader.GetString(entity, mapping.UserId),
            DisplayName = DataverseEntityReader.GetString(entity, mapping.DisplayName),
            EmailAddress = DataverseEntityReader.GetString(entity, mapping.EmailAddress),
            Department = DataverseEntityReader.GetString(entity, mapping.Department),
            Manager = DataverseEntityReader.GetString(entity, mapping.Manager),
            Status = DataverseEntityReader.GetString(entity, mapping.Status)
        };
    }
}
