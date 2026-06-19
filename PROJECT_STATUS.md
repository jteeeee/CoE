# Project Status

## Current Phase

Phase 1 foundation is in progress.

The backend foundation, Dataverse integration scaffolding, API readiness work, and mock-data React frontend are in place. The frontend currently runs without a working backend, Dataverse connection, or Entra ID authentication.

## Completed

### Documentation

- Project documentation is stored in `/docs`.
- Current documentation covers requirements, architecture, data model, roadmap, engineering standards, AI guidelines, and UI guidelines.

### Backend

- Created .NET 8 Clean Architecture solution under `/backend`.
- Added Domain, Application, Infrastructure, and API layers.
- Added dependency injection setup for Application and Infrastructure layers.
- Added Serilog configuration.
- Added health endpoints:
  - `GET /api/health`
  - `GET /health`
- Added canonical domain entities from `docs/datamodel.md`.
- Added repository interfaces for applications, flows, environments, users, governance records, and risk records.
- Added Application services for dashboard, applications, flows, and environments.
- Added Dataverse client and repository implementations for:
  - Applications
  - Flows
  - Environments
  - Users
- Added Dataverse mapping configuration and mapping validation scaffolding.
- Added REST API controllers for dashboard, applications, flows, environments, and Dataverse mapping validation.
- Added DTO mapping so Dataverse entities are not exposed directly to the UI.
- Added paging, search, filtering, sorting, and CSV export contracts for inventory endpoints.
- Added global exception middleware and standard API error responses.

### Frontend

- Created React frontend under `/frontend`.
- Added TypeScript, Material UI, React Query, React Router, and Recharts.
- Added MUI light/dark theme support.
- Added application shell with fixed header and collapsible navigation.
- Added Phase 1 pages:
  - Dashboard
  - Applications
  - Flows
  - Environments
- Added reusable components:
  - `PageHeader`
  - `MetricCard`
  - `TrendCard`
  - `RiskBadge`
  - `StatusBadge`
  - `HealthIndicator`
  - `DataTable`
  - `EmptyState`
  - `ErrorState`
- Added realistic mock governance data in `/frontend/src/mock`.
- Added frontend service interfaces and mock service implementation.
- Added React Query hooks so pages call services instead of importing mock data directly.

## Not Implemented Yet

- Entra ID authentication and frontend MSAL integration.
- Role-based authorisation.
- Real frontend API client connected to the .NET API.
- Governance and Administration frontend pages.
- Governance and risk application services.
- Governance and risk repository implementations.
- Connector domain entity and connector repository.
- Dataverse-side query pushdown for paging, search, filtering, and sorting.
- Production CoE logical table and column validation against the target tenant.
- Domain-specific exception types and mappings.
- Route-based frontend code splitting.

## Build Status

### Frontend

Status: Passing.

Verified command:

```bash
cd frontend
pnpm build
```

Result:

```text
TypeScript check and Vite production build completed successfully.
```

Note:

Vite reports a non-blocking initial bundle size warning. Add route-based code splitting before production hardening.

### Backend

Status: Not verified in this Mac workspace.

Reason:

The .NET SDK is not available on the current PATH in this environment.

Expected verification command on a machine with .NET 8 installed:

```bash
dotnet build backend/PowerPlatformGovernance.sln
```

## Runtime Notes

- The Phase 1 frontend uses mock data only.
- The frontend does not require the backend, Dataverse, or Entra ID to run locally.
- Dataverse configuration values in `appsettings.json` are placeholders until validated against the target tenant.
