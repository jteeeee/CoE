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
public sealed class EnvironmentsController(IEnvironmentService environmentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResultDto<EnvironmentDto>>> GetAll(
        [FromQuery] EnvironmentInventoryRequest request,
        CancellationToken cancellationToken)
    {
        var environments = await environmentService.GetEnvironmentsAsync(
            request.ToQuery(),
            cancellationToken);

        return Ok(DtoMapper.ToDto(environments, DtoMapper.ToDto));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EnvironmentDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var environment = await environmentService.GetEnvironmentByIdAsync(id, cancellationToken);

        return environment is null
            ? NotFound(ApiErrorResponses.NotFound(HttpContext, "Environment was not found."))
            : Ok(DtoMapper.ToDto(environment));
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export(
        [FromQuery] EnvironmentExportRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Format != ExportFormat.Csv)
        {
            return BadRequest(ApiErrorResponses.BadRequest(
                HttpContext,
                "Only CSV export is currently supported."));
        }

        var environments = await environmentService.GetEnvironmentsForExportAsync(
            request.ToInventoryQuery(),
            cancellationToken);
        var csv = CsvExportBuilder.Build(environments.Select(DtoMapper.ToDto).ToArray(), request.Columns);

        return File(
            System.Text.Encoding.UTF8.GetBytes(csv),
            "text/csv",
            $"environments-{DateTime.UtcNow:yyyyMMddHHmmss}.csv");
    }
}
