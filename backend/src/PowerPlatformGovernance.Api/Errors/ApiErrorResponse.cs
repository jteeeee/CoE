namespace PowerPlatformGovernance.Api.Errors;

public sealed record ApiErrorResponse(
    int StatusCode,
    string Code,
    string Message,
    string TraceId,
    IReadOnlyDictionary<string, string[]>? Errors = null);
