namespace PowerPlatformGovernance.Api.Dto.Administration;

public sealed record DataverseMappingValidationDto(
    bool IsValid,
    IReadOnlyCollection<DataverseTableMappingValidationDto> Tables);

public sealed record DataverseTableMappingValidationDto(
    string MappingName,
    string TableName,
    bool TableExists,
    bool IsValid,
    IReadOnlyCollection<string> ConfiguredColumns,
    IReadOnlyCollection<string> MissingColumns,
    string? ErrorMessage);
