namespace PowerPlatformGovernance.Domain.Entities;

public sealed class Flow
{
    public Guid FlowId { get; init; }

    public required string FlowName { get; init; }

    public Guid EnvironmentId { get; init; }

    public required string OwnerId { get; init; }

    public required string OwnerName { get; init; }

    public DateTime CreatedDate { get; init; }

    public DateTime ModifiedDate { get; init; }

    public DateTime LastRunDate { get; init; }

    public int FailureCount { get; init; }

    public decimal SuccessRate { get; init; }

    public required string Status { get; init; }
}
