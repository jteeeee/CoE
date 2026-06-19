# UI Guidelines

## Purpose

This document defines the user experience, visual design standards, interaction patterns and component standards for the Power Platform Governance Portal.

The purpose of this document is to ensure:

- Consistent user experience
- Consistent layouts
- Consistent component usage
- Predictable AI-generated code
- Professional enterprise administration interface

All frontend development must comply with this document.

---

# Design Philosophy

The Governance Portal is a modern enterprise platform.

The application should feel:

- Modern
- Premium
- Intelligent
- Fast
- Enterprise Ready
- Executive Ready

The user experience should communicate platform maturity and governance capability.

The application should feel similar to:

- Microsoft Fabric
- Azure Portal
- Datadog
- Grafana Cloud
- Vercel Dashboard
- Linear
- Atlassian Compass

Avoid designs that resemble:

- Traditional corporate intranets
- Legacy administration tools
- Generic CRUD applications

The interface should feel like a strategic platform product rather than an internal utility.

---

# Design Principles

## Consistency

Similar actions should behave identically across all screens.

Tables, forms, filters and navigation should follow common patterns.

---

## Simplicity

Avoid unnecessary visual complexity.

Prefer:

- Simple layouts
- Clear navigation
- Minimal visual noise

Avoid:

- Complex animations
- Decorative graphics
- Excessive whitespace

---

## Efficiency

Common tasks should require minimal clicks.

Users should be able to:

- Search quickly
- Filter quickly
- Navigate quickly

---

## Accessibility

All components must meet accessibility standards.

Requirements:

- Keyboard navigation
- Screen reader support
- Colour contrast compliance
- Focus indicators

---

# Technology Standards

Framework:

- React
- TypeScript

UI Library:

- Material UI (MUI)

Icons:

- Material Icons

Charts:

- Recharts

State Management:

- React Query

Routing:

- React Router

---

# Application Layout

## Standard Layout

Every page should use the same application shell.

Structure:

Header

↓

Left Navigation

↓

Page Content

---

## Header

Fixed at top.

Contains:

- Application Name
- Current User
- Notifications
- User Menu

Height:

64px

---

## Navigation

Position:

Left side

Behaviour:

- Expandable
- Collapsible

Sections:

Dashboard

Applications

Flows

Environments

Governance

Administration

---

## Content Area

Primary workspace.

Contains:

- Page Header
- Actions
- Filters
- Content

Maximum Width:

None

Application should use available screen width.

---

# Page Structure

Every page follows the same pattern.

## Page Header

Contains:

- Page Title
- Description
- Page Actions

Example:

Applications

Search and manage Power Platform applications.

[Export]
[Refresh]

---

## Filter Section

Position:

Below page header

Contains:

- Search
- Filters
- Status selectors

Consistent across all inventory screens.

---

## Main Content

Contains:

- Table
- Cards
- Charts
- Forms

---


# Dashboard Standards

The dashboard is the hero experience of the platform.

It should immediately communicate:

- Platform Scale
- Platform Health
- Governance Coverage
- Risk Exposure

Dashboard Structure

Row 1

KPI Cards

- Applications
- Flows
- Environments
- Makers

Row 2

Operational Health

- Application Trends
- Flow Trends
- Environment Distribution

Row 3

Governance

- Ownership Coverage
- Governance Compliance
- Review Status

Row 4

Risk

- Orphaned Assets
- High Risk Assets
- Failed Flows

Row 5

Recent Activity

- Latest Changes
- Governance Events
- Operational Alerts

Dashboard should favour insight and storytelling over raw metrics.

---
# Executive Demonstration Standards

The application will be demonstrated to:

- Technology Leadership
- Enterprise Architecture
- Governance Teams
- Senior Stakeholders

Every screen should be presentation quality.

When designing screens:

Prioritise:

- Visual hierarchy
- Clean spacing
- Consistent typography
- Meaningful visualisations

Avoid:

- Empty screens
- Large tables without summary insights
- Excessive text
- Unstyled layouts

Each major page should contain:

- Summary metrics
- Actionable insights
- Detailed information

The application should look investment-worthy and strategically important.

---

## Summary Cards

Display:

- Total Applications
- Total Flows
- Total Environments
- Total Makers

Each card contains:

Title

Primary Metric

Trend Indicator

Icon

---

## Dashboard Cards

Consistent height.

Consistent spacing.

No custom styling per card.

---

# Table Standards

Tables are the primary user interface pattern.

All inventory screens must use a shared table component.

---

## Required Features

Search

Sorting

Filtering

Pagination

Export

Column Selection

Refresh

---

## Standard Table Actions

Top Right:

Refresh

Export

Column Settings

---

## Standard Columns

Text fields left aligned.

Numeric fields right aligned.

Dates formatted consistently.

---

## Row Actions

View

Edit

Delete (where applicable)

Actions displayed using overflow menu.

---

# Search Standards

Global search component.

Position:

Top left of table toolbar.

Supports:

- Partial matches
- Exact matches
- Case insensitive search

Debounce:

300 milliseconds

---

# Filter Standards

Filters appear above table.

Examples:

Status

Environment

Owner

Risk Level

Criticality

Filters must be reusable components.

---

# Form Standards

Use Material UI forms.

All forms follow same layout.

---

## Form Layout

Label

Input

Helper Text

Validation Message

---

## Validation

Display validation immediately after interaction.

Do not rely solely on server-side validation.

---

## Required Fields

Marked using:

*

Example:

Application Name *

---

# Detail Page Standards

Every asset type should have a consistent detail page.

---

## Detail Layout

Section 1

Summary

Section 2

Ownership

Section 3

Dependencies

Section 4

Governance

Section 5

Audit Information

---

## Summary Section

Display key information prominently.

Examples:

Name

Owner

Environment

Status

Last Updated

---

# Card Standards

Use Material UI cards.

Cards should be:

- Flat
- Minimal
- Consistent

Avoid excessive custom styling.

---

# Theme & Visual Identity

Theme Framework:

Material UI Theme System

Support:

- Light Mode
- Dark Mode

Default:

Light Mode

Visual Style:

- Modern
- Clean
- Minimal
- Data Focused

Design Tokens

Border Radius:
12px

Card Radius:
16px

Spacing:
8px grid system

Shadow:
Subtle elevation only

Animation:
150ms - 250ms transitions

Avoid:

- Sharp corners
- Heavy borders
- Outdated enterprise styling
- Excessive gradients

Cards should use elevation and spacing rather than borders to create hierarchy.

---

# Typography

Font:

Roboto

Hierarchy:

H1 Page Title

H2 Section Title

Body Text

Caption

Use Material UI defaults.

---

# Spacing Standards

Base Unit:

8px

Common Values:

8px

16px

24px

32px

Use consistent spacing throughout application.

---

# Loading States

Every data request must display a loading state.

Use:

Skeleton loaders

or

Progress indicators

Never leave blank screens.

---

# Empty States

Every table and dashboard component must handle empty states.

Example:

"No applications found."

Provide action where appropriate.

---

# Error States

Display user-friendly messages.

Example:

Unable to load applications.

Please try again later.

Avoid exposing technical details.

---

# Notifications

Use Material UI Snackbar.

Types:

Success

Warning

Error

Information

Position:

Bottom Right

---

# Responsive Design

Support:

Desktop

Laptop

Tablet

Mobile support is not a priority.

Design primarily for enterprise desktop users.

---

# Dark Mode

Architecture should support dark mode.

Initial release:

Light mode only.

---

# Reusable Components

The following components must be shared.

ApplicationShell

PageHeader

DataTable

SearchBar

FilterPanel

MetricCard

DetailSection

ConfirmationDialog

LoadingIndicator

EmptyState

ErrorPanel

---

# Naming Standards

Pages:

ApplicationsPage

FlowsPage

DashboardPage

Components:

ApplicationTable

FlowTable

MetricCard

Services:

ApplicationService

FlowService

GovernanceService

---

# Anti-Patterns

Do Not Use:

Custom CSS frameworks

Inline styling

Large custom component libraries

Multiple table implementations

Multiple navigation layouts

Custom animation libraries

Marketing-style UI patterns

---

# Success Criteria

Users should be able to:

- Find an application within seconds
- Identify orphaned assets quickly
- Review governance information efficiently
- Navigate the platform intuitively

The interface should feel similar to:

- Azure Portal
- Power Platform Admin Centre
- Microsoft Fabric

while remaining simpler and easier to use.