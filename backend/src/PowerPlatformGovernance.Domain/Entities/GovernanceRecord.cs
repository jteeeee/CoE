namespace PowerPlatformGovernance.Domain.Entities;

public sealed class GovernanceRecord
{
    public Guid GovernanceId { get; init; }

    public required string AssetType { get; init; }

    public Guid AssetId { get; init; }

    public required string BusinessOwner { get; init; }

    public required string SupportTeam { get; init; }

    public required string Criticality { get; init; }

    public required string LifecycleStatus { get; init; }

    public DateTime ReviewDate { get; init; }

    public required string Comments { get; init; }
}
