namespace PowerPlatformGovernance.Api.Dto;

public sealed record ApplicationDto(
    Guid AppId,
    string AppName,
    Guid EnvironmentId,
    string EnvironmentName,
    string OwnerId,
    string OwnerName,
    string OwnerEmail,
    DateTime CreatedDate,
    DateTime ModifiedDate,
    DateTime LastUsedDate,
    int UserCount,
    string Status,
    string RiskRating,
    string BusinessCriticality,
    string SupportTeam);
