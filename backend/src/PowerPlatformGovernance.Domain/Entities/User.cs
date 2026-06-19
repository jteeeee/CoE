namespace PowerPlatformGovernance.Domain.Entities;

public sealed class User
{
    public required string UserId { get; init; }

    public required string DisplayName { get; init; }

    public required string EmailAddress { get; init; }

    public required string Department { get; init; }

    public required string Manager { get; init; }

    public required string Status { get; init; }
}
