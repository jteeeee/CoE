namespace PowerPlatformGovernance.Infrastructure.Dataverse.Validation;

public interface IDataverseMappingValidator
{
    Task<DataverseMappingValidationResult> ValidateAsync(CancellationToken cancellationToken = default);
}
