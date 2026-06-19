namespace PowerPlatformGovernance.Domain.Entities;

public sealed class Environment
{
    public Guid EnvironmentId { get; init; }

    public required string EnvironmentName { get; init; }

    public required string EnvironmentType { get; init; }

    public required string Region { get; init; }

    public decimal CapacityUsedMb { get; init; }

    public decimal CapacityAllocatedMb { get; init; }

    public int AppCount { get; init; }

    public int FlowCount { get; init; }

    public int MakerCount { get; init; }

    public required string DlpPolicyName { get; init; }
}
