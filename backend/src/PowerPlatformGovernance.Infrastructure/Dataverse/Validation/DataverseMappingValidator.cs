using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PowerPlatformGovernance.Infrastructure.Dataverse.Validation;

public sealed class DataverseMappingValidator(
    IDataverseClient dataverseClient,
    IOptions<DataverseOptions> options,
    ILogger<DataverseMappingValidator> logger) : IDataverseMappingValidator
{
    public async Task<DataverseMappingValidationResult> ValidateAsync(
        CancellationToken cancellationToken = default)
    {
        var tableMappings = GetConfiguredTableMappings(options.Value).ToArray();
        var results = new List<DataverseTableMappingValidationResult>();

        foreach (var tableMapping in tableMappings)
        {
            results.Add(await ValidateTableAsync(tableMapping, cancellationToken));
        }

        return new DataverseMappingValidationResult(
            results.All(result => result.IsValid),
            results);
    }

    private async Task<DataverseTableMappingValidationResult> ValidateTableAsync(
        ConfiguredTableMapping tableMapping,
        CancellationToken cancellationToken)
    {
        try
        {
            var metadata = await dataverseClient.RetrieveEntityMetadataAsync(
                tableMapping.TableName,
                cancellationToken);
            var actualColumns = metadata.Attributes
                .Select(attribute => attribute.LogicalName)
                .Where(logicalName => !string.IsNullOrWhiteSpace(logicalName))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            var missingColumns = tableMapping.Columns
                .Where(column => !actualColumns.Contains(column))
                .ToArray();

            return new DataverseTableMappingValidationResult(
                tableMapping.MappingName,
                tableMapping.TableName,
                TableExists: true,
                tableMapping.Columns,
                missingColumns,
                ErrorMessage: null);
        }
        catch (Exception exception)
        {
            logger.LogWarning(
                exception,
                "Dataverse mapping validation failed for {MappingName} table {TableName}",
                tableMapping.MappingName,
                tableMapping.TableName);

            return new DataverseTableMappingValidationResult(
                tableMapping.MappingName,
                tableMapping.TableName,
                TableExists: false,
                tableMapping.Columns,
                tableMapping.Columns,
                exception.Message);
        }
    }

    private static IEnumerable<ConfiguredTableMapping> GetConfiguredTableMappings(DataverseOptions options)
    {
        yield return new ConfiguredTableMapping(
            "Applications",
            options.Tables.Applications.TableName,
            [
                options.Tables.Applications.AppId,
                options.Tables.Applications.AppName,
                options.Tables.Applications.EnvironmentId,
                options.Tables.Applications.EnvironmentName,
                options.Tables.Applications.OwnerId,
                options.Tables.Applications.OwnerName,
                options.Tables.Applications.OwnerEmail,
                options.Tables.Applications.CreatedDate,
                options.Tables.Applications.ModifiedDate,
                options.Tables.Applications.LastUsedDate,
                options.Tables.Applications.UserCount,
                options.Tables.Applications.Status,
                options.Tables.Applications.RiskRating,
                options.Tables.Applications.BusinessCriticality,
                options.Tables.Applications.SupportTeam
            ]);

        yield return new ConfiguredTableMapping(
            "Flows",
            options.Tables.Flows.TableName,
            [
                options.Tables.Flows.FlowId,
                options.Tables.Flows.FlowName,
                options.Tables.Flows.EnvironmentId,
                options.Tables.Flows.OwnerId,
                options.Tables.Flows.OwnerName,
                options.Tables.Flows.CreatedDate,
                options.Tables.Flows.ModifiedDate,
                options.Tables.Flows.LastRunDate,
                options.Tables.Flows.FailureCount,
                options.Tables.Flows.SuccessRate,
                options.Tables.Flows.Status
            ]);

        yield return new ConfiguredTableMapping(
            "Environments",
            options.Tables.Environments.TableName,
            [
                options.Tables.Environments.EnvironmentId,
                options.Tables.Environments.EnvironmentName,
                options.Tables.Environments.EnvironmentType,
                options.Tables.Environments.Region,
                options.Tables.Environments.CapacityUsedMb,
                options.Tables.Environments.CapacityAllocatedMb,
                options.Tables.Environments.AppCount,
                options.Tables.Environments.FlowCount,
                options.Tables.Environments.MakerCount,
                options.Tables.Environments.DlpPolicyName
            ]);

        yield return new ConfiguredTableMapping(
            "Users",
            options.Tables.Users.TableName,
            [
                options.Tables.Users.UserId,
                options.Tables.Users.DisplayName,
                options.Tables.Users.EmailAddress,
                options.Tables.Users.Department,
                options.Tables.Users.Manager,
                options.Tables.Users.Status
            ]);
    }

    private sealed record ConfiguredTableMapping(
        string MappingName,
        string TableName,
        IReadOnlyCollection<string> Columns);
}
