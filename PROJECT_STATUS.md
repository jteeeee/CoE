# Project Status

## Current Phase

Dataverse infrastructure integration scaffolded.

## Completed

- Moved project planning documentation into `/docs`.
- Created `/backend` solution structure for .NET 8 Clean Architecture.
- Created Domain, Application, Infrastructure, and API projects.
- Added project references that preserve layer boundaries:
  - API references Application and Infrastructure.
  - Infrastructure references Application and Domain.
  - Application references Domain.
  - Domain has no project references.
- Added dependency injection extension points for Application and Infrastructure.
- Configured Serilog in API startup.
- Added controller-based health endpoint at `GET /api/health`.
- Added ASP.NET Core health check endpoint at `GET /health`.
- Added Domain entities from `docs/datamodel.md`:
  - `Application`
  - `Flow`
  - `Environment`
  - `User`
  - `GovernanceRecord`
  - `RiskRecord`
- Added Domain repository interfaces:
  - `IApplicationRepository`
  - `IFlowRepository`
  - `IEnvironmentRepository`
  - `IUserRepository`
  - `IGovernanceRepository`
  - `IRiskRepository`
- Added Application-layer services that depend on Domain repository interfaces:
  - `DashboardService`
  - `ApplicationService`
  - `FlowService`
  - `EnvironmentService`
- Added service interfaces:
  - `IDashboardService`
  - `IApplicationService`
  - `IFlowService`
  - `IEnvironmentService`
- Added `DashboardSummary` application model.
- Registered Application services in `AddApplication()`.
- Added Dataverse SDK package reference:
  - `Microsoft.PowerPlatform.Dataverse.Client` version `1.2.10`.
- Added service-principal Dataverse client wrapper:
  - `DataverseClient`
  - `IDataverseClient`
- Added configuration model for Dataverse access and CoE table mappings:
  - `DataverseOptions`
  - `DataverseTableMappings`
  - `ApplicationTableMapping`
  - `FlowTableMapping`
  - `EnvironmentTableMapping`
- Added Dataverse entity reader helpers for canonical type conversion.
- Added Dataverse repository implementations:
  - `DataverseApplicationRepository`
  - `DataverseFlowRepository`
  - `DataverseEnvironmentRepository`
- Registered Dataverse client and repositories in Infrastructure dependency injection.
- Added placeholder Dataverse configuration in `appsettings.json`.

## Not Implemented Yet

- API controllers for dashboard, applications, flows, and environments.
- Full feature business rules beyond initial dashboard aggregation.
- `IUserRepository` Dataverse implementation.
- Governance and risk repository implementations.
- Authentication and authorisation.
- Governance application service.
- Risk application service.
- Connector entity and `IConnectorRepository`.
- Production CoE logical table and column names must be validated against the target tenant and adjusted in configuration if needed.

## Build Status

Build verification is blocked in this environment because the .NET SDK is not installed on the current PATH.

Attempted command:

```bash
dotnet build backend/PowerPlatformGovernance.sln
```

Result:

```text
zsh:1: command not found: dotnet
```

Additional check:

```bash
which -a dotnet
```

Result:

```text
dotnet not found
```
