using PowerPlatformGovernance.Application.Queries;

namespace PowerPlatformGovernance.Api.Requests;

public sealed class FlowExportRequest
{
    public string? Search { get; set; }

    public Guid? EnvironmentId { get; set; }

    public string? OwnerId { get; set; }

    public string? Status { get; set; }

    public int? MinimumFailureCount { get; set; }

    public string? SortBy { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

    public ExportFormat Format { get; set; } = ExportFormat.Csv;

    public string[] Columns { get; set; } = [];

    public FlowInventoryQuery ToInventoryQuery()
    {
        return new FlowInventoryQuery
        {
            Search = Search,
            EnvironmentId = EnvironmentId,
            OwnerId = OwnerId,
            Status = Status,
            MinimumFailureCount = MinimumFailureCount,
            SortBy = SortBy,
            SortDirection = SortDirection
        };
    }
}
