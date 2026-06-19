using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace PowerPlatformGovernance.Infrastructure.Dataverse;

public interface IDataverseClient
{
    Task<IReadOnlyCollection<Entity>> RetrieveAllAsync(
        QueryExpression query,
        CancellationToken cancellationToken = default);
}
