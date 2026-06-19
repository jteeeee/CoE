using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PowerPlatformGovernance.Api.Errors;

internal static class ApiErrorResponses
{
    public static ApiErrorResponse BadRequest(
        HttpContext httpContext,
        string message,
        IReadOnlyDictionary<string, string[]>? errors = null)
    {
        return Create(
            httpContext,
            StatusCodes.Status400BadRequest,
            "BadRequest",
            message,
            errors);
    }

    public static ApiErrorResponse NotFound(HttpContext httpContext, string message)
    {
        return Create(
            httpContext,
            StatusCodes.Status404NotFound,
            "NotFound",
            message);
    }

    public static ApiErrorResponse InternalServerError(
        HttpContext httpContext,
        string message)
    {
        return Create(
            httpContext,
            StatusCodes.Status500InternalServerError,
            "InternalServerError",
            message);
    }

    public static ApiErrorResponse ValidationFailed(
        HttpContext httpContext,
        ModelStateDictionary modelState)
    {
        var errors = modelState
            .Where(item => item.Value?.Errors.Count > 0)
            .ToDictionary(
                item => item.Key,
                item => item.Value?.Errors
                    .Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                        ? "The value is invalid."
                        : error.ErrorMessage)
                    .ToArray() ?? []);

        return BadRequest(httpContext, "Request validation failed.", errors);
    }

    private static ApiErrorResponse Create(
        HttpContext httpContext,
        int statusCode,
        string code,
        string message,
        IReadOnlyDictionary<string, string[]>? errors = null)
    {
        return new ApiErrorResponse(
            statusCode,
            code,
            message,
            httpContext.TraceIdentifier,
            errors);
    }
}
