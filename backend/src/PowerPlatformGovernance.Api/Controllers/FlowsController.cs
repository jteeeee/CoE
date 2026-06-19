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
public sealed class FlowsController(IFlowService flowService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResultDto<FlowDto>>> GetAll(
        [FromQuery] FlowInventoryRequest request,
        CancellationToken cancellationToken)
    {
        var flows = await flowService.GetFlowsAsync(request.ToQuery(), cancellationToken);

        return Ok(DtoMapper.ToDto(flows, DtoMapper.ToDto));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FlowDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var flow = await flowService.GetFlowByIdAsync(id, cancellationToken);

        return flow is null
            ? NotFound(ApiErrorResponses.NotFound(HttpContext, "Flow was not found."))
            : Ok(DtoMapper.ToDto(flow));
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export(
        [FromQuery] FlowExportRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Format != ExportFormat.Csv)
        {
            return BadRequest(ApiErrorResponses.BadRequest(
                HttpContext,
                "Only CSV export is currently supported."));
        }

        var flows = await flowService.GetFlowsForExportAsync(request.ToInventoryQuery(), cancellationToken);
        var csv = CsvExportBuilder.Build(flows.Select(DtoMapper.ToDto).ToArray(), request.Columns);

        return File(
            System.Text.Encoding.UTF8.GetBytes(csv),
            "text/csv",
            $"flows-{DateTime.UtcNow:yyyyMMddHHmmss}.csv");
    }
}
