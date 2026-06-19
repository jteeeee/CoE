using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace PowerPlatformGovernance.Infrastructure.Dataverse;

internal static class DataverseEntityReader
{
    public static ColumnSet BuildColumnSet(params string[] columns)
    {
        return new ColumnSet(columns.Where(column => !string.IsNullOrWhiteSpace(column)).Distinct().ToArray());
    }

    public static ConditionExpression BuildIdCondition(string column, Guid id)
    {
        return new ConditionExpression(column, ConditionOperator.Equal, id);
    }

    public static Guid GetGuid(Entity entity, string columnName)
    {
        var value = GetValue(entity, columnName);

        return value switch
        {
            Guid guid => guid,
            EntityReference entityReference => entityReference.Id,
            string text when Guid.TryParse(text, out var guid) => guid,
            _ when string.Equals(entity.LogicalName + "id", columnName, StringComparison.OrdinalIgnoreCase)
                => entity.Id,
            _ => Guid.Empty
        };
    }

    public static string GetString(Entity entity, string columnName)
    {
        var value = GetValue(entity, columnName);

        return value switch
        {
            null => string.Empty,
            string text => text,
            EntityReference entityReference => entityReference.Name ?? entityReference.Id.ToString(),
            OptionSetValue optionSetValue => optionSetValue.Value.ToString(),
            Money money => money.Value.ToString("F2"),
            DateTime dateTime => dateTime.ToString("O"),
            _ => value.ToString() ?? string.Empty
        };
    }

    public static DateTime GetDateTime(Entity entity, string columnName)
    {
        var value = GetValue(entity, columnName);

        return value switch
        {
            DateTime dateTime => dateTime,
            string text when DateTime.TryParse(text, out var dateTime) => dateTime,
            _ => DateTime.MinValue
        };
    }

    public static int GetInt(Entity entity, string columnName)
    {
        var value = GetValue(entity, columnName);

        return value switch
        {
            int number => number,
            long number => Convert.ToInt32(number),
            decimal number => Convert.ToInt32(number),
            double number => Convert.ToInt32(number),
            OptionSetValue optionSetValue => optionSetValue.Value,
            string text when int.TryParse(text, out var number) => number,
            _ => 0
        };
    }

    public static decimal GetDecimal(Entity entity, string columnName)
    {
        var value = GetValue(entity, columnName);

        return value switch
        {
            decimal number => number,
            double number => Convert.ToDecimal(number),
            int number => number,
            long number => number,
            Money money => money.Value,
            string text when decimal.TryParse(text, out var number) => number,
            _ => 0
        };
    }

    private static object? GetValue(Entity entity, string columnName)
    {
        if (!entity.Attributes.TryGetValue(columnName, out var value))
        {
            return null;
        }

        return value is AliasedValue aliasedValue ? aliasedValue.Value : value;
    }
}
