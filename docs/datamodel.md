# Power Platform Governance Portal - Data Model

## Purpose

This document defines the canonical governance model used by the Governance Portal.

The portal must consume data from CoE Starter Kit tables today and support Fabric Lakehouse in the future.

Frontend and API layers must only use the canonical model defined in this document.

Source system schemas must never be exposed directly to consumers.

---

# Core Entities

## Application

Represents a Power App.

### Fields

| Field | Type |
|---------|---------|
| AppId | Guid |
| AppName | String |
| EnvironmentId | Guid |
| EnvironmentName | String |
| OwnerId | String |
| OwnerName | String |
| OwnerEmail | String |
| CreatedDate | DateTime |
| ModifiedDate | DateTime |
| LastUsedDate | DateTime |
| UserCount | Integer |
| Status | String |
| RiskRating | String |
| BusinessCriticality | String |
| SupportTeam | String |

---

## Flow

Represents a Power Automate flow.

### Fields

| Field | Type |
|---------|---------|
| FlowId | Guid |
| FlowName | String |
| EnvironmentId | Guid |
| OwnerId | String |
| OwnerName | String |
| CreatedDate | DateTime |
| ModifiedDate | DateTime |
| LastRunDate | DateTime |
| FailureCount | Integer |
| SuccessRate | Decimal |
| Status | String |

---

## Environment

Represents a Power Platform environment.

### Fields

| Field | Type |
|---------|---------|
| EnvironmentId | Guid |
| EnvironmentName | String |
| EnvironmentType | String |
| Region | String |
| CapacityUsedMb | Decimal |
| CapacityAllocatedMb | Decimal |
| AppCount | Integer |
| FlowCount | Integer |
| MakerCount | Integer |
| DlpPolicyName | String |

---

## User

Represents an owner or maker.

### Fields

| Field | Type |
|---------|---------|
| UserId | String |
| DisplayName | String |
| EmailAddress | String |
| Department | String |
| Manager | String |
| Status | String |

---

## Connector

Represents a Power Platform connector.

### Fields

| Field | Type |
|---------|---------|
| ConnectorId | Guid |
| ConnectorName | String |
| ConnectorType | String |
| EnvironmentId | Guid |
| UsageCount | Integer |

---

# Governance Entities

## Governance Record

Custom metadata maintained by platform team.

### Fields

| Field | Type |
|---------|---------|
| GovernanceId | Guid |
| AssetType | String |
| AssetId | Guid |
| BusinessOwner | String |
| SupportTeam | String |
| Criticality | String |
| LifecycleStatus | String |
| ReviewDate | DateTime |
| Comments | String |

---

## Risk Record

### Fields

| Field | Type |
|---------|---------|
| RiskId | Guid |
| AssetId | Guid |
| AssetType | String |
| RiskLevel | String |
| RiskCategory | String |
| Description | String |
| ReviewDate | DateTime |

---

# Dashboard Metrics

## Applications

Calculated:

- Total Applications
- Active Applications
- Inactive Applications
- Orphaned Applications
- High Risk Applications

---

## Flows

Calculated:

- Total Flows
- Failed Flows
- Inactive Flows
- Orphaned Flows

---

## Environments

Calculated:

- Total Environments
- Production Environments
- Sandbox Environments
- Capacity Utilisation %

---

# Relationships

Environment
    |
    +-- Applications
    |
    +-- Flows
    |
    +-- Connectors

Application
    |
    +-- Governance Record
    |
    +-- Risk Records

Flow
    |
    +-- Governance Record
    |
    +-- Risk Records

User
    |
    +-- Applications
    |
    +-- Flows

---

# Repository Interfaces

IApplicationRepository

IFlowRepository

IEnvironmentRepository

IConnectorRepository

IUserRepository

IGovernanceRepository

IRiskRepository

---

# Future Fabric Mapping

The following sources may populate the model:

Applications
    -> CoE Tables
    -> Power Platform Admin API

Flows
    -> CoE Tables
    -> Power Platform Admin API

Users
    -> CoE Tables
    -> Microsoft Graph

Environments
    -> CoE Tables
    -> Power Platform Admin API

Semantic Models
    -> Power BI API

Fabric Assets
    -> Fabric API

Governance Records
    -> Custom Governance Tables

No frontend component may depend on source-specific schemas.

All source systems must map into the canonical model before exposure to the API layer.
