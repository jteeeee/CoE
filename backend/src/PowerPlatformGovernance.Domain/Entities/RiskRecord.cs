namespace PowerPlatformGovernance.Domain.Entities;

public sealed class RiskRecord
{
    public Guid RiskId { get; init; }

    public Guid AssetId { get; init; }

    public required string AssetType { get; init; }

    public required string RiskLevel { get; init; }

    public required string RiskCategory { get; init; }

    public required string Description { get; init; }

    public DateTime ReviewDate { get; init; }
}
