namespace PowerPlatformGovernance.Infrastructure.Dataverse.Validation;

public sealed record DataverseMappingValidationResult(
    bool IsValid,
    IReadOnlyCollection<DataverseTableMappingValidationResult> Tables);

public sealed record DataverseTableMappingValidationResult(
    string MappingName,
    string TableName,
    bool TableExists,
    IReadOnlyCollection<string> ConfiguredColumns,
    IReadOnlyCollection<string> MissingColumns,
    string? ErrorMessage)
{
    public bool IsValid => TableExists && MissingColumns.Count == 0 && string.IsNullOrWhiteSpace(ErrorMessage);
}
