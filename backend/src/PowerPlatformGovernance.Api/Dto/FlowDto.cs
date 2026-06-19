namespace PowerPlatformGovernance.Api.Dto;

public sealed record FlowDto(
    Guid FlowId,
    string FlowName,
    Guid EnvironmentId,
    string OwnerId,
    string OwnerName,
    DateTime CreatedDate,
    DateTime ModifiedDate,
    DateTime LastRunDate,
    int FailureCount,
    decimal SuccessRate,
    string Status);
