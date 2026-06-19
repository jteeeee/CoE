# Project Status

## Current Phase

Phase 1 React frontend scaffold completed with mock governance data.

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
  - `UserTableMapping`
- Added Dataverse entity reader helpers for canonical type conversion.
- Added Dataverse repository implementations:
  - `DataverseApplicationRepository`
  - `DataverseFlowRepository`
  - `DataverseEnvironmentRepository`
  - `DataverseUserRepository`
- Registered Dataverse client and repositories in Infrastructure dependency injection.
- Added placeholder Dataverse configuration in `appsettings.json`.
- Resolved `DashboardService` runtime dependency completeness by registering `IUserRepository`.
- Added API response DTOs:
  - `ApplicationDto`
  - `FlowDto`
  - `EnvironmentDto`
  - `DashboardDto`
- Added API DTO mapping helpers.
- Added thin REST API controllers:
  - `DashboardController`
  - `ApplicationsController`
  - `FlowsController`
  - `EnvironmentsController`
- Added documented application search endpoint at `GET /api/applications/search`.
- Added Application-layer search method so search logic does not live in controllers.
- Added shared paging contract:
  - `PagedResult<T>`
  - `PagedResultDto<T>`
- Added inventory query contracts for frontend table workflows:
  - `ApplicationInventoryQuery`
  - `FlowInventoryQuery`
  - `EnvironmentInventoryQuery`
- Added API query request contracts:
  - `ApplicationInventoryRequest`
  - `FlowInventoryRequest`
  - `EnvironmentInventoryRequest`
- Added export request contracts:
  - `ApplicationExportRequest`
  - `FlowExportRequest`
  - `EnvironmentExportRequest`
- Added sort and export enums:
  - `SortDirection`
  - `ExportFormat`
- Updated Applications, Flows, and Environments endpoints to support paging, search, filters, and sorting.
- Added CSV export endpoints:
  - `GET /api/applications/export`
  - `GET /api/flows/export`
  - `GET /api/environments/export`
- Added API-layer CSV export formatter.
- Added consistent API error response contract:
  - `ApiErrorResponse`
  - `ApiErrorResponses`
- Added global exception middleware:
  - `ExceptionHandlingMiddleware`
- Registered exception middleware in API startup.
- Configured model validation failures to return the standard API error response shape.
- Updated controller `NotFound` and `BadRequest` responses to use the standard API error response shape.
- Created `/frontend` React application using:
  - React
  - TypeScript
  - Material UI
  - React Query
  - React Router
  - Recharts
- Added Vite build configuration and strict TypeScript configuration.
- Added MUI theme factory with light and dark mode support.
- Added application shell with fixed header, collapsible left navigation, current user area, notifications action, and theme toggle.
- Added Phase 1 pages:
  - Dashboard
  - Applications
  - Flows
  - Environments
- Added reusable frontend components:
  - `PageHeader`
  - `MetricCard`
  - `TrendCard`
  - `RiskBadge`
  - `StatusBadge`
  - `HealthIndicator`
  - `DataTable`
  - `EmptyState`
  - `ErrorState`
- Added mock governance data in a dedicated frontend mock folder.
- Added frontend service interface and mock service implementation so pages do not import mock data directly.
- Added React Query hooks for dashboard, applications, flows, environments, governance records, and risk records.
- Added shared formatting and CSV export utilities.
- Added `.gitignore` entries for frontend dependencies, build output, and backend build artifacts.

## Not Implemented Yet

- Full feature business rules beyond initial dashboard aggregation.
- Dataverse-side query pushdown for paging, search, filtering, and sorting.
- Governance and risk repository implementations.
- Authentication and authorisation.
- Governance application service.
- Risk application service.
- Connector entity and `IConnectorRepository`.
- Domain-specific exception types and mappings.
- Production CoE logical table and column names must be validated against the target tenant and adjusted in configuration if needed.
- Frontend authentication with MSAL.
- Frontend connection to the .NET API.
- Real API client implementation behind the `GovernanceApi` interface.
- Route-based frontend code splitting to reduce the initial bundle size.
- Governance and Administration frontend pages.

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

Frontend build verification succeeded.

Command:

```bash
cd frontend
pnpm build
```

Result:

```text
tsc -p tsconfig.json --noEmit && tsc -p tsconfig.node.json --noEmit && vite build completed successfully.
```

Note:

Vite reported a chunk-size warning for the initial frontend bundle. This does not block the Phase 1 build, but route-based code splitting should be added before production hardening.
