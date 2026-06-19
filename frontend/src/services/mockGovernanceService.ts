import {
  applications,
  dashboardSummary,
  environments,
  flows,
  governanceRecords,
  riskRecords
} from "../mock/governanceData";
import type { Application, Environment, Flow, GovernanceRecord, InventoryQuery, RiskRecord } from "../types/models";
import type { GovernanceApi } from "./interfaces";

const responseDelayMs = 180;

const wait = async () => new Promise((resolve) => window.setTimeout(resolve, responseDelayMs));

const matches = (value: string | number | undefined, search: string) =>
  String(value ?? "")
    .toLocaleLowerCase()
    .includes(search.toLocaleLowerCase());

const normalise = (value: string | undefined) => value?.trim().toLocaleLowerCase() ?? "";

const filterApplications = (items: Application[], query?: InventoryQuery) => {
  const search = normalise(query?.search);

  return items.filter((item) => {
    const matchesSearch =
      !search ||
      matches(item.appName, search) ||
      matches(item.environmentName, search) ||
      matches(item.ownerName, search) ||
      matches(item.supportTeam, search);

    const matchesStatus = !query?.status || item.status === query.status;
    const matchesEnvironment = !query?.environment || item.environmentName === query.environment;
    const matchesRisk = !query?.riskLevel || item.riskRating === query.riskLevel;
    const matchesOwner = !query?.owner || item.ownerName === query.owner;

    return matchesSearch && matchesStatus && matchesEnvironment && matchesRisk && matchesOwner;
  });
};

const filterFlows = (items: Flow[], query?: InventoryQuery) => {
  const search = normalise(query?.search);

  return items.filter((item) => {
    const environmentName = environments.find((environment) => environment.environmentId === item.environmentId)?.environmentName;
    const matchesSearch =
      !search || matches(item.flowName, search) || matches(item.ownerName, search) || matches(environmentName, search);

    const matchesStatus = !query?.status || item.status === query.status;
    const matchesEnvironment = !query?.environment || environmentName === query.environment;
    const matchesOwner = !query?.owner || item.ownerName === query.owner;

    return matchesSearch && matchesStatus && matchesEnvironment && matchesOwner;
  });
};

const filterEnvironments = (items: Environment[], query?: InventoryQuery) => {
  const search = normalise(query?.search);

  return items.filter((item) => {
    const matchesSearch =
      !search ||
      matches(item.environmentName, search) ||
      matches(item.environmentType, search) ||
      matches(item.region, search) ||
      matches(item.dlpPolicyName, search);

    const matchesStatus = !query?.status || item.environmentType === query.status;
    const matchesEnvironment = !query?.environment || item.environmentName === query.environment;

    return matchesSearch && matchesStatus && matchesEnvironment;
  });
};

export class MockGovernanceService implements GovernanceApi {
  async getDashboardSummary() {
    await wait();
    return dashboardSummary;
  }

  async getApplications(query?: InventoryQuery) {
    await wait();
    return filterApplications(applications, query);
  }

  async getFlows(query?: InventoryQuery) {
    await wait();
    return filterFlows(flows, query);
  }

  async getEnvironments(query?: InventoryQuery) {
    await wait();
    return filterEnvironments(environments, query);
  }

  async getGovernanceRecords(): Promise<GovernanceRecord[]> {
    await wait();
    return governanceRecords;
  }

  async getRiskRecords(): Promise<RiskRecord[]> {
    await wait();
    return riskRecords;
  }
}
