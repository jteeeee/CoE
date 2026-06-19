namespace PowerPlatformGovernance.Infrastructure.Dataverse;

public sealed class DataverseOptions
{
    public const string SectionName = "Dataverse";

    public required string Url { get; init; }

    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public DataverseTableMappings Tables { get; init; } = new();
}

public sealed class DataverseTableMappings
{
    public ApplicationTableMapping Applications { get; init; } = new();

    public FlowTableMapping Flows { get; init; } = new();

    public EnvironmentTableMapping Environments { get; init; } = new();

    public UserTableMapping Users { get; init; } = new();
}

public sealed class ApplicationTableMapping
{
    public string TableName { get; init; } = "admin_powerapp";

    public string AppId { get; init; } = "admin_appid";

    public string AppName { get; init; } = "admin_displayname";

    public string EnvironmentId { get; init; } = "admin_environmentid";

    public string EnvironmentName { get; init; } = "admin_environmentname";

    public string OwnerId { get; init; } = "admin_ownerid";

    public string OwnerName { get; init; } = "admin_ownername";

    public string OwnerEmail { get; init; } = "admin_owneremail";

    public string CreatedDate { get; init; } = "createdon";

    public string ModifiedDate { get; init; } = "modifiedon";

    public string LastUsedDate { get; init; } = "admin_lastuseddate";

    public string UserCount { get; init; } = "admin_usercount";

    public string Status { get; init; } = "statecode";

    public string RiskRating { get; init; } = "admin_riskrating";

    public string BusinessCriticality { get; init; } = "admin_businesscriticality";

    public string SupportTeam { get; init; } = "admin_supportteam";
}

public sealed class FlowTableMapping
{
    public string TableName { get; init; } = "admin_flow";

    public string FlowId { get; init; } = "admin_flowid";

    public string FlowName { get; init; } = "admin_displayname";

    public string EnvironmentId { get; init; } = "admin_environmentid";

    public string OwnerId { get; init; } = "admin_ownerid";

    public string OwnerName { get; init; } = "admin_ownername";

    public string CreatedDate { get; init; } = "createdon";

    public string ModifiedDate { get; init; } = "modifiedon";

    public string LastRunDate { get; init; } = "admin_lastrundate";

    public string FailureCount { get; init; } = "admin_failurecount";

    public string SuccessRate { get; init; } = "admin_successrate";

    public string Status { get; init; } = "statecode";
}

public sealed class EnvironmentTableMapping
{
    public string TableName { get; init; } = "admin_environment";

    public string EnvironmentId { get; init; } = "admin_environmentid";

    public string EnvironmentName { get; init; } = "admin_displayname";

    public string EnvironmentType { get; init; } = "admin_environmenttype";

    public string Region { get; init; } = "admin_region";

    public string CapacityUsedMb { get; init; } = "admin_capacityusedmb";

    public string CapacityAllocatedMb { get; init; } = "admin_capacityallocatedmb";

    public string AppCount { get; init; } = "admin_appcount";

    public string FlowCount { get; init; } = "admin_flowcount";

    public string MakerCount { get; init; } = "admin_makercount";

    public string DlpPolicyName { get; init; } = "admin_dlppolicyname";
}

public sealed class UserTableMapping
{
    public string TableName { get; init; } = "admin_maker";

    public string UserId { get; init; } = "admin_makerid";

    public string DisplayName { get; init; } = "admin_displayname";

    public string EmailAddress { get; init; } = "admin_email";

    public string Department { get; init; } = "admin_department";

    public string Manager { get; init; } = "admin_manager";

    public string Status { get; init; } = "statecode";
}
