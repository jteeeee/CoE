using PowerPlatformGovernance.Application.Queries;

namespace PowerPlatformGovernance.Api.Requests;

public sealed class ApplicationExportRequest
{
    public string? Search { get; set; }

    public Guid? EnvironmentId { get; set; }

    public string? OwnerId { get; set; }

    public string? Status { get; set; }

    public string? RiskRating { get; set; }

    public string? SortBy { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

    public ExportFormat Format { get; set; } = ExportFormat.Csv;

    public string[] Columns { get; set; } = [];

    public ApplicationInventoryQuery ToInventoryQuery()
    {
        return new ApplicationInventoryQuery
        {
            Search = Search,
            EnvironmentId = EnvironmentId,
            OwnerId = OwnerId,
            Status = Status,
            RiskRating = RiskRating,
            SortBy = SortBy,
            SortDirection = SortDirection
        };
    }
}
