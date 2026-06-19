using Microsoft.AspNetCore.Mvc;
using PowerPlatformGovernance.Api.Dto;
using PowerPlatformGovernance.Api.Errors;
using PowerPlatformGovernance.Api.Export;
using PowerPlatformGovernance.Api.Requests;
using PowerPlatformGovernance.Application.Queries;
using PowerPlatformGovernance.Application.Services;

namespace PowerPlatformGovernance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ApplicationsController(IApplicationService applicationService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResultDto<ApplicationDto>>> GetAll(
        [FromQuery] ApplicationInventoryRequest request,
        CancellationToken cancellationToken)
    {
        var applications = await applicationService.GetApplicationsAsync(
            request.ToQuery(),
            cancellationToken);

        return Ok(DtoMapper.ToDto(applications, DtoMapper.ToDto));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var application = await applicationService.GetApplicationByIdAsync(id, cancellationToken);

        return application is null
            ? NotFound(ApiErrorResponses.NotFound(HttpContext, "Application was not found."))
            : Ok(DtoMapper.ToDto(application));
    }

    [HttpGet("search")]
    public async Task<ActionResult<PagedResultDto<ApplicationDto>>> Search(
        [FromQuery] string? query,
        [FromQuery] ApplicationInventoryRequest request,
        CancellationToken cancellationToken)
    {
        request.Search ??= query;
        var applications = await applicationService.GetApplicationsAsync(
            request.ToQuery(),
            cancellationToken);

        return Ok(DtoMapper.ToDto(applications, DtoMapper.ToDto));
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export(
        [FromQuery] ApplicationExportRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Format != ExportFormat.Csv)
        {
            return BadRequest(ApiErrorResponses.BadRequest(
                HttpContext,
                "Only CSV export is currently supported."));
        }

        var applications = await applicationService.GetApplicationsForExportAsync(
            request.ToInventoryQuery(),
            cancellationToken);
        var csv = CsvExportBuilder.Build(applications.Select(DtoMapper.ToDto).ToArray(), request.Columns);

        return File(
            System.Text.Encoding.UTF8.GetBytes(csv),
            "text/csv",
            $"applications-{DateTime.UtcNow:yyyyMMddHHmmss}.csv");
    }
}
