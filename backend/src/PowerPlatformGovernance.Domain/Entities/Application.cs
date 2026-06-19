namespace PowerPlatformGovernance.Domain.Entities;

public sealed class Application
{
    public Guid AppId { get; init; }

    public required string AppName { get; init; }

    public Guid EnvironmentId { get; init; }

    public required string EnvironmentName { get; init; }

    public required string OwnerId { get; init; }

    public required string OwnerName { get; init; }

    public required string OwnerEmail { get; init; }

    public DateTime CreatedDate { get; init; }

    public DateTime ModifiedDate { get; init; }

    public DateTime LastUsedDate { get; init; }

    public int UserCount { get; init; }

    public required string Status { get; init; }

    public required string RiskRating { get; init; }

    public required string BusinessCriticality { get; init; }

    public required string SupportTeam { get; init; }
}
