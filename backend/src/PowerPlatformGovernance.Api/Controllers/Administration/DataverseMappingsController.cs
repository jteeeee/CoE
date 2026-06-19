using Microsoft.AspNetCore.Mvc;
using PowerPlatformGovernance.Api.Dto.Administration;
using PowerPlatformGovernance.Infrastructure.Dataverse.Validation;

namespace PowerPlatformGovernance.Api.Controllers.Administration;

[ApiController]
[Route("api/administration/dataverse-mappings")]
public sealed class DataverseMappingsController(
    IDataverseMappingValidator dataverseMappingValidator) : ControllerBase
{
    [HttpGet("validate")]
    public async Task<ActionResult<DataverseMappingValidationDto>> Validate(
        CancellationToken cancellationToken)
    {
        var result = await dataverseMappingValidator.ValidateAsync(cancellationToken);

        return Ok(new DataverseMappingValidationDto(
            result.IsValid,
            result.Tables
                .Select(table => new DataverseTableMappingValidationDto(
                    table.MappingName,
                    table.TableName,
                    table.TableExists,
                    table.IsValid,
                    table.ConfiguredColumns,
                    table.MissingColumns,
                    table.ErrorMessage))
                .ToArray()));
    }
}
