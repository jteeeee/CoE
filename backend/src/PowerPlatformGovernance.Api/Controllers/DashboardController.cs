using Microsoft.AspNetCore.Mvc;
using PowerPlatformGovernance.Api.Dto;
using PowerPlatformGovernance.Application.Services;

namespace PowerPlatformGovernance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DashboardController(IDashboardService dashboardService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<DashboardDto>> Get(CancellationToken cancellationToken)
    {
        var dashboard = await dashboardService.GetSummaryAsync(cancellationToken);

        return Ok(DtoMapper.ToDto(dashboard));
    }
}
