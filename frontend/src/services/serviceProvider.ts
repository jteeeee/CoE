import { MockGovernanceService } from "./mockGovernanceService";
import type { GovernanceApi } from "./interfaces";

export const governanceService: GovernanceApi = new MockGovernanceService();
