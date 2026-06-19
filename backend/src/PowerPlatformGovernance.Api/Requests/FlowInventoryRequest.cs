using PowerPlatformGovernance.Application.Queries;

namespace PowerPlatformGovernance.Api.Requests;

public sealed class FlowInventoryRequest
{
    public string? Search { get; set; }

    public Guid? EnvironmentId { get; set; }

    public string? OwnerId { get; set; }

    public string? Status { get; set; }

    public int? MinimumFailureCount { get; set; }

    public string? SortBy { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;

    public FlowInventoryQuery ToQuery()
    {
        return new FlowInventoryQuery
        {
            Search = Search,
            EnvironmentId = EnvironmentId,
            OwnerId = OwnerId,
            Status = Status,
            MinimumFailureCount = MinimumFailureCount,
            SortBy = SortBy,
            SortDirection = SortDirection,
            Page = Page,
            PageSize = PageSize
        };
    }
}
