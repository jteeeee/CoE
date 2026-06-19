using System.Reflection;
using System.Text;

namespace PowerPlatformGovernance.Api.Export;

internal static class CsvExportBuilder
{
    public static string Build<T>(IReadOnlyCollection<T> records, string[] requestedColumns)
    {
        var properties = ResolveProperties<T>(requestedColumns);
        var builder = new StringBuilder();

        builder.AppendLine(string.Join(',', properties.Select(property => Escape(property.Name))));

        foreach (var record in records)
        {
            var values = properties.Select(property => Escape(property.GetValue(record)?.ToString() ?? string.Empty));
            builder.AppendLine(string.Join(',', values));
        }

        return builder.ToString();
    }

    private static IReadOnlyCollection<PropertyInfo> ResolveProperties<T>(string[] requestedColumns)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

        if (requestedColumns.Length == 0)
        {
            return properties;
        }

        var requestedColumnSet = requestedColumns.ToHashSet(StringComparer.OrdinalIgnoreCase);

        return properties
            .Where(property => requestedColumnSet.Contains(property.Name))
            .ToArray();
    }

    private static string Escape(string value)
    {
        var mustQuote = value.Contains(',')
            || value.Contains('"')
            || value.Contains('\n')
            || value.Contains('\r');

        if (!mustQuote)
        {
            return value;
        }

        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
