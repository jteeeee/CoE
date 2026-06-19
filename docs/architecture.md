# Power Platform Governance Portal - Architecture

## Solution Overview

The Governance Portal provides a central administration and governance platform for Power Platform assets.

The solution consumes metadata from CoE Starter Kit Dataverse tables and exposes it through a modern React user interface.

The architecture is designed to support future migration to Power BI APIs, Fabric APIs and a Fabric Lakehouse without requiring frontend changes.

---

# High Level Architecture

React Frontend

↓

.NET Web API

↓

Application Layer

↓

Repository Layer

↓

Dataverse / CoE Tables

Future:

↓

Fabric Lakehouse

↓

Power Platform APIs

↓

Power BI APIs

↓

Microsoft Graph

---

# Architectural Principles

## Separation of Concerns

Frontend

Responsible for:

- User Experience
- Visualisation
- Search
- Filtering

Backend

Responsible for:

- Business Logic
- Security
- Data Aggregation
- Data Transformation

Data Sources

Responsible for:

- Metadata Storage
- Platform Information

---

## Source Agnostic Design

The frontend must never know:

- CoE Table Names
- Dataverse Schema
- Fabric Schema

The frontend consumes only API DTOs.

---

## API First Design

All functionality must be exposed through REST APIs.

Future integrations must consume existing APIs rather than bypassing them.

---

# Solution Structure

text /src    /frontend      /components     /pages     /layouts     /services     /hooks     /types    /backend      /Api     /Application     /Domain     /Infrastructure  /docs 

---

# Backend Architecture

## API Layer

Responsibilities:

- Authentication
- Authorisation
- Request Validation
- Response Formatting

Examples:

DashboardController

ApplicationsController

FlowsController

EnvironmentsController

GovernanceController

---

## Application Layer

Responsibilities:

- Business Logic
- Aggregation Logic
- Governance Rules

Examples:

ApplicationService

FlowService

DashboardService

GovernanceService

---

## Domain Layer

Contains:

Entities

Value Objects

Interfaces

Business Rules

Must not depend on infrastructure.

---

## Infrastructure Layer

Contains:

Dataverse Repositories

API Clients

External Integrations

Logging

Configuration

---

# Repository Pattern

Interfaces:

IApplicationRepository

IFlowRepository

IEnvironmentRepository

IUserRepository

IGovernanceRepository

IRiskRepository

Implementations:

DataverseApplicationRepository

DataverseFlowRepository

DataverseEnvironmentRepository

Future:

FabricApplicationRepository

FabricFlowRepository

PowerBiRepository

GraphRepository

---

# Authentication

Provider:

Microsoft Entra ID

Authentication Flow:

User

↓

React

↓

Entra Login

↓

JWT Token

↓

.NET API

↓

Authorised Request

---

# Authorisation

Roles:

Platform Administrator

Governance Administrator

Read Only User

Platform Administrator

Full Access

Governance Administrator

Governance Updates

Read Only User

View Only

---

# Frontend Architecture

Framework:

React

Language:

TypeScript

UI Framework:

Material UI

State Management:

React Query

Routing:

React Router

Authentication:

MSAL

---

# Frontend Folder Structure

text /frontend    /components      /common     /dashboard     /applications     /flows     /environments    /pages      Dashboard     Applications     Flows     Environments     Governance    /services      apiClient     appService     flowService     governanceService    /types    /hooks 

---

# API Endpoints

## Dashboard

GET /api/dashboard

Returns:

- Summary Metrics
- Risk Metrics
- Capacity Metrics

---

## Applications

GET /api/applications

GET /api/applications/{id}

GET /api/applications/search

---

## Flows

GET /api/flows

GET /api/flows/{id}

---

## Environments

GET /api/environments

GET /api/environments/{id}

---

## Governance

GET /api/governance

POST /api/governance

PUT /api/governance/{id}

DELETE /api/governance/{id}

---

# Dataverse Integration

Initial Source:

CoE Starter Kit Dataverse Tables

Access Method:

Dataverse SDK

Service Principal Authentication

Read Only

No direct updates to CoE tables.

---

# Custom Governance Tables

Separate Dataverse tables:

GovernanceRecord

RiskRecord

ReviewRecord

SupportAssignment

Do not modify Microsoft-owned CoE tables.

---

# Logging

Framework:

ILogger

Log:

Authentication Failures

Dataverse Errors

API Exceptions

Governance Changes

---

# Monitoring

Future:

Application Insights

Azure Monitor

Custom Governance Metrics

---

# Deployment

Initial

React Static App

↓

Azure App Service

↓

.NET API

↓

Dataverse

Future

React Static App

↓

Azure App Service

↓

.NET API

↓

Fabric Lakehouse

↓

Microsoft APIs

---

# Future State

Current

React

↓

.NET API

↓

Dataverse CoE

Future

React

↓

.NET API

↓

Fabric Lakehouse

↓

Power Platform APIs

↓

Power BI APIs

↓

Microsoft Graph

Frontend remains unchanged.

Repository implementations change.

Business services remain unchanged.

This ensures low-risk migration from CoE to Fabric.