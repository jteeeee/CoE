# Power Platform Governance Portal - Product Roadmap

## Vision

Provide a single governance, administration and observability platform for the organisation's Power Platform, Power BI and Microsoft Fabric ecosystem.

The platform will become the authoritative source for:

- Platform inventory
- Ownership
- Governance
- Operational health
- Risk management
- Dependency mapping
- Lifecycle management

The platform will initially leverage CoE Starter Kit data and progressively transition to direct Microsoft APIs and Fabric.

---

# Guiding Principles

## Business First

The platform exists to answer business and governance questions, not simply display technical metadata.

Examples:

- Who owns this application?
- Is this application still used?
- What breaks if we retire this application?
- Is this application compliant?
- Who supports this application?

---

## Single Source of Truth

The portal becomes the governance layer regardless of the underlying data source.

Current:
- CoE Dataverse

Future:
- Power Platform APIs
- Microsoft Graph
- Power BI APIs
- Fabric APIs

---

## API First

All functionality must be available through APIs.

Frontend applications consume APIs only.

---

## Source Agnostic

Frontend must never depend on:

- CoE table names
- Dataverse schemas
- Fabric schemas

All sources map into the canonical governance model.

---

# Phase 1 - Foundation

Target Duration: 4 to 6 Weeks

## Objectives

Deliver immediate value using existing CoE data.

## Features

### Dashboard

Display:

- Total Apps
- Total Flows
- Total Environments
- Total Makers
- Orphaned Assets
- Disabled Owner Assets
- Inactive Assets
- Failed Flows

### Application Inventory

Searchable inventory of:

- Apps
- Owners
- Usage
- Status

### Flow Inventory

Searchable inventory of:

- Flows
- Owners
- Success Rates
- Failures

### Environment Inventory

Display:

- Capacity
- App Counts
- Flow Counts
- DLP Policies

### Governance Register

Capture:

- Business Owner
- Support Team
- Criticality
- Lifecycle Status
- Review Date

### Security

- Entra ID Authentication
- Role Based Access

## Success Criteria

Platform team can answer:

- What exists?
- Who owns it?
- Is it being used?

---

# Phase 2 - Governance & Ownership

Target Duration: 4 Weeks

## Objectives

Introduce operational governance.

## Integrations

### Microsoft Graph

Retrieve:

- Users
- Departments
- Managers
- Group Membership

## Features

### Orphaned Asset Detection

Identify:

- Disabled Users
- Deleted Users
- Missing Owners

### Ownership Validation

Detect:

- Shared Mailboxes
- Generic Accounts
- Unsupported Assets

### Governance Reviews

Support:

- Review Cycles
- Approval Workflows
- Ownership Attestations

### Risk Register

Maintain:

- Risk Ratings
- Mitigations
- Review History

## Success Criteria

Platform team can answer:

- Who owns this?
- Is ownership valid?
- Is support assigned?

---

# Phase 3 - Power BI Governance

Target Duration: 4 to 6 Weeks

## Objectives

Extend governance beyond Power Platform.

## Integrations

### Power BI REST API

Retrieve:

- Workspaces
- Reports
- Dashboards
- Semantic Models
- Refresh History

## Features

### Semantic Model Inventory

Track:

- Owners
- Data Sources
- Refresh Status

### Workspace Governance

Track:

- Workspace Owners
- Workspace Usage
- Workspace Risk

### Report Inventory

Track:

- Report Ownership
- Report Usage
- Report Dependencies

## Success Criteria

Platform team can answer:

- Which reports use this dataset?
- Who owns this workspace?
- Is this dataset healthy?

---

# Phase 4 - Fabric Governance

Target Duration: 6 Weeks

## Objectives

Support Fabric assets.

## Integrations

### Fabric APIs

Retrieve:

- Workspaces
- Lakehouses
- Warehouses
- Pipelines
- Notebooks
- Semantic Models

## Features

### Fabric Asset Inventory

Track:

- Asset Ownership
- Usage
- Health

### Data Lineage

Track:

- Source Systems
- Data Movement
- Downstream Consumers

### Capacity Monitoring

Track:

- Fabric Capacity
- Consumption
- Utilisation Trends

## Success Criteria

Platform team can answer:

- What Fabric assets exist?
- Who owns them?
- What depends on them?

---

# Phase 5 - Dependency Mapping

Target Duration: 6 to 8 Weeks

## Objectives

Create a dependency graph.

## Features

### Dependency Discovery

Map:

- App -> Flow
- App -> Dataverse
- Flow -> Dataverse
- Dataset -> Source
- Report -> Dataset
- Fabric -> Dataset

### Impact Analysis

Answer:

- What breaks if removed?
- Who is impacted?
- What systems are affected?

### Relationship Explorer

Visualise:

- Applications
- Data Sources
- Reports
- Fabric Assets

## Success Criteria

Platform team can answer:

- What depends on this asset?
- What is the blast radius of change?

---

# Phase 6 - Operational Excellence

Target Duration: Ongoing

## Features

### Health Scoring

Calculate:

- Ownership Score
- Usage Score
- Supportability Score
- Compliance Score

### Technical Debt Scoring

Identify:

- Legacy Assets
- Unused Assets
- Unsupported Assets

### Governance Alerts

Notify:

- Owners
- Support Teams
- Platform Administrators

### Lifecycle Management

Manage:

- New
- Active
- Under Review
- Retired

## Success Criteria

Platform team can proactively manage risk rather than react to incidents.

---

# Future AI Capabilities

## AI Asset Summaries

Generate plain English summaries:

- Purpose
- Owner
- Dependencies
- Risks

## AI Risk Detection

Identify:

- Orphaned Assets
- Unused Assets
- Duplicate Assets
- Governance Gaps

## AI Dependency Analysis

Explain:

- Upstream Dependencies
- Downstream Dependencies
- Potential Impact

## AI Governance Assistant

Natural language queries:

- Show orphaned apps
- Show inactive production flows
- Show high-risk assets without owners
- Show applications due for review

---

# Long-Term Architecture

Current

React
-> .NET API
-> CoE Dataverse

Phase 3

React
-> .NET API
-> CoE Dataverse
-> Power BI APIs
-> Microsoft Graph

Phase 4+

React
-> .NET API
-> Fabric Lakehouse
-> Power Platform APIs
-> Power BI APIs
-> Fabric APIs
-> Microsoft Graph

The user experience remains unchanged while the backend evolves toward a unified governance platform.