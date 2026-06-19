# Power Platform Governance Portal - Engineering Standards

## Technology Stack

Frontend:
- React
- TypeScript
- Material UI
- React Query
- React Router

Backend:
- .NET 8 Web API
- Entity Framework Core
- Dataverse SDK

Authentication:
- Microsoft Entra ID
- MSAL

Database:
- Dataverse (Initial Phase)
- Fabric Lakehouse (Future)

---

# Architecture Principles

## Clean Architecture

Layers:

- API
- Application
- Domain
- Infrastructure

Domain layer must not depend on infrastructure.

---

# API Design

RESTful APIs.

Examples:

GET /api/apps

GET /api/apps/{id}

GET /api/flows

GET /api/environments

GET /api/dashboard

GET /api/governance

POST /api/governance

PUT /api/governance/{id}

---

# Dataverse Access

Use Service Principal authentication.

Read-only access to CoE tables.

Repository pattern for data access.

Never expose Dataverse entities directly to UI.

Always map to DTOs.

---

# Coding Standards

## General

- SOLID principles
- Dependency Injection
- Async/Await everywhere
- No business logic in controllers

## Naming

Classes:
PascalCase

Methods:
PascalCase

Variables:
camelCase

Interfaces:
Prefix with I

Example:

IAppRepository

AppRepository

---

# Frontend Standards

Use:

- Functional Components
- React Hooks
- TypeScript Strict Mode

Avoid:

- Class Components
- Inline API Calls

All API calls through service layer.

---

# Frontend Quality Standards

The frontend is a strategic showcase application.

When multiple implementation options exist, prefer:

- Better user experience
- Better visual design
- Better component reusability
- Better perceived quality

Avoid generating generic CRUD interfaces.

The application should feel like a modern SaaS platform rather than a traditional internal business application.

## Component Standards

Create reusable components for:

- MetricCard
- TrendCard
- RiskBadge
- StatusBadge
- HealthIndicator
- AssetSummaryCard
- ActivityTimeline
- GovernanceScoreCard
- EmptyState
- ErrorState

All pages should be assembled from reusable components rather than custom page-specific implementations.

## Dashboard Standards

The dashboard is the primary showcase experience.

The dashboard should communicate:

- Platform Health
- Platform Scale
- Governance Coverage
- Risk Exposure
- Recent Activity

The dashboard should prioritise visual storytelling over simple metric display.

## Theme Standards

Support:

- Light Theme
- Dark Theme

All components must support both themes.

Do not hardcode colours.

Use design tokens and theme variables.

## Animation Standards

Use subtle transitions:

- Hover Effects
- Card Elevation
- Navigation Transitions

Animation duration:

150ms to 250ms

Avoid excessive motion or decorative animation.

---

# UI Principles

Provide:

- Fast navigation
- Consistent layout
- Enterprise administration experience

Design for:

- Platform teams
- Governance teams
- Technology managers

Not citizen developers.

The application is an enterprise administration platform.

Prioritise:

- Information density
- Readability
- Consistency
- Accessibility

Avoid:

- Marketing layouts
- Excessive whitespace
- Large hero sections
- Complex animations

All pages must follow a common layout.

Use Material UI components where possible.

Tables are the primary information display pattern.

All inventory pages should share a common table component.

All detail pages should share a common detail page layout.

---

# Error Handling

Backend:

- Global Exception Middleware

Frontend:

- Error Boundaries
- User Friendly Messages

Log all unexpected errors.

---

# Logging

Use:

ILogger

Log:

- API Requests
- Authentication Failures
- Dataverse Failures
- Governance Changes

---

# Testing

Backend:

- xUnit
- FluentAssertions

Frontend:

- Vitest
- React Testing Library

Target:

80% coverage minimum.

---

# Future Migration Strategy

Current:

React
-> .NET API
-> Dataverse CoE Tables

Future:

React
-> .NET API
-> Fabric Lakehouse

Frontend must remain unchanged during migration.

Repository layer abstracts data source implementation.

Never couple UI directly to Dataverse schema.
