namespace PowerPlatformGovernance.Api.Dto;

public sealed record EnvironmentDto(
    Guid EnvironmentId,
    string EnvironmentName,
    string EnvironmentType,
    string Region,
    decimal CapacityUsedMb,
    decimal CapacityAllocatedMb,
    int AppCount,
    int FlowCount,
    int MakerCount,
    string DlpPolicyName);
