namespace PowerPlatformGovernance.Application.Queries;

public sealed class ApplicationInventoryQuery
{
    public string? Search { get; init; }

    public Guid? EnvironmentId { get; init; }

    public string? OwnerId { get; init; }

    public string? Status { get; init; }

    public string? RiskRating { get; init; }

    public string? SortBy { get; init; }

    public SortDirection SortDirection { get; init; } = SortDirection.Ascending;

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 50;
}
