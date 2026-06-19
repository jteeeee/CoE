using PowerPlatformGovernance.Application.Queries;

namespace PowerPlatformGovernance.Api.Requests;

public sealed class EnvironmentInventoryRequest
{
    public string? Search { get; set; }

    public string? EnvironmentType { get; set; }

    public string? Region { get; set; }

    public string? DlpPolicyName { get; set; }

    public string? SortBy { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;

    public EnvironmentInventoryQuery ToQuery()
    {
        return new EnvironmentInventoryQuery
        {
            Search = Search,
            EnvironmentType = EnvironmentType,
            Region = Region,
            DlpPolicyName = DlpPolicyName,
            SortBy = SortBy,
            SortDirection = SortDirection,
            Page = Page,
            PageSize = PageSize
        };
    }
}
