namespace PowerPlatformGovernance.Application.Common;

public sealed record PagedResult<T>(
    IReadOnlyCollection<T> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage)
{
    public static PagedResult<T> Create(
        IEnumerable<T> source,
        int page,
        int pageSize)
    {
        var normalizedPage = Math.Max(page, 1);
        var normalizedPageSize = Math.Clamp(pageSize, 1, 500);
        var items = source.ToArray();
        var totalCount = items.Length;
        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)normalizedPageSize);

        var pagedItems = items
            .Skip((normalizedPage - 1) * normalizedPageSize)
            .Take(normalizedPageSize)
            .ToArray();

        return new PagedResult<T>(
            pagedItems,
            normalizedPage,
            normalizedPageSize,
            totalCount,
            totalPages,
            normalizedPage > 1,
            totalPages > 0 && normalizedPage < totalPages);
    }
}
