namespace PowerPlatformGovernance.Application.Queries;

public sealed class EnvironmentInventoryQuery
{
    public string? Search { get; init; }

    public string? EnvironmentType { get; init; }

    public string? Region { get; init; }

    public string? DlpPolicyName { get; init; }

    public string? SortBy { get; init; }

    public SortDirection SortDirection { get; init; } = SortDirection.Ascending;

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 50;
}
