using PowerPlatformGovernance.Application.Queries;

namespace PowerPlatformGovernance.Api.Requests;

public sealed class ApplicationInventoryRequest
{
    public string? Search { get; set; }

    public Guid? EnvironmentId { get; set; }

    public string? OwnerId { get; set; }

    public string? Status { get; set; }

    public string? RiskRating { get; set; }

    public string? SortBy { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;

    public ApplicationInventoryQuery ToQuery()
    {
        return new ApplicationInventoryQuery
        {
            Search = Search,
            EnvironmentId = EnvironmentId,
            OwnerId = OwnerId,
            Status = Status,
            RiskRating = RiskRating,
            SortBy = SortBy,
            SortDirection = SortDirection,
            Page = Page,
            PageSize = PageSize
        };
    }
}
