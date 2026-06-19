using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace PowerPlatformGovernance.Infrastructure.Dataverse;

public sealed class DataverseClient : IDataverseClient, IDisposable
{
    private const int PageSize = 5000;

    private readonly ILogger<DataverseClient> logger;
    private readonly ServiceClient serviceClient;

    public DataverseClient(
        IOptions<DataverseOptions> options,
        ILogger<DataverseClient> logger)
    {
        this.logger = logger;

        var dataverseOptions = options.Value;
        var connectionString = BuildConnectionString(dataverseOptions);

        serviceClient = new ServiceClient(connectionString);

        if (!serviceClient.IsReady)
        {
            var message = string.IsNullOrWhiteSpace(serviceClient.LastError)
                ? "Dataverse service client failed to initialise."
                : serviceClient.LastError;

            throw new InvalidOperationException(message, serviceClient.LastException);
        }
    }

    public async Task<IReadOnlyCollection<Entity>> RetrieveAllAsync(
        QueryExpression query,
        CancellationToken cancellationToken = default)
    {
        var results = new List<Entity>();
        var pageNumber = 1;
        string? pagingCookie = null;

        query.PageInfo = new PagingInfo
        {
            Count = PageSize,
            PageNumber = pageNumber,
            PagingCookie = pagingCookie
        };

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            logger.LogDebug(
                "Retrieving Dataverse page {PageNumber} for table {TableName}",
                pageNumber,
                query.EntityName);

            var response = await Task.Run(
                () => serviceClient.RetrieveMultiple(query),
                cancellationToken);

            results.AddRange(response.Entities);

            if (!response.MoreRecords)
            {
                return results;
            }

            pageNumber++;
            pagingCookie = response.PagingCookie;
            query.PageInfo.PageNumber = pageNumber;
            query.PageInfo.PagingCookie = pagingCookie;
        }
    }

    public void Dispose()
    {
        serviceClient.Dispose();
    }

    private static string BuildConnectionString(DataverseOptions options)
    {
        return string.Join(
            ';',
            "AuthType=ClientSecret",
            $"Url={options.Url}",
            $"ClientId={options.ClientId}",
            $"ClientSecret={options.ClientSecret}");
    }
}
