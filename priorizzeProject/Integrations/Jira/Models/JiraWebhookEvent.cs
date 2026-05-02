using System.Text.Json;

namespace priorizzeProject.Integrations.Jira.Models;

public sealed class JiraWebhookEvent
{
    public string EventType { get; init; } = string.Empty;
    public string? IssueKey { get; init; }
    public JsonElement Payload { get; init; }

    public static JiraWebhookEvent FromPayload(JsonElement payload)
    {
        return new JiraWebhookEvent
        {
            EventType = GetString(payload, "webhookEvent") ?? GetString(payload, "issue_event_type_name") ?? string.Empty,
            IssueKey = GetNestedString(payload, "issue", "key"),
            Payload = payload
        };
    }

    public bool IsMetricHistoryOrCycleEvent()
    {
        return ContainsIgnoredTerm(EventType)
            || ContainsIgnoredTerm(GetNestedString(Payload, "issue", "fields", "issuetype", "name"))
            || ContainsIgnoredTerm(GetNestedString(Payload, "issue", "fields", "summary"));
    }

    public static string? GetString(JsonElement element, string propertyName)
    {
        return element.ValueKind == JsonValueKind.Object
            && element.TryGetProperty(propertyName, out var property)
            && property.ValueKind == JsonValueKind.String
                ? property.GetString()
                : null;
    }

    public static string? GetNestedString(JsonElement element, params string[] path)
    {
        var current = element;

        foreach (var segment in path)
        {
            if (current.ValueKind != JsonValueKind.Object || !current.TryGetProperty(segment, out current))
            {
                return null;
            }
        }

        return current.ValueKind == JsonValueKind.String ? current.GetString() : current.ToString();
    }

    public static int GetNestedInt(JsonElement element, int fallback, params string[] path)
    {
        var value = GetNestedString(element, path);
        return int.TryParse(value, out var parsed) ? parsed : fallback;
    }

    public static double? GetNestedDouble(JsonElement element, params string[] path)
    {
        var value = GetNestedString(element, path);
        return double.TryParse(value, out var parsed) ? parsed : null;
    }

    public static DateTimeOffset GetNestedDateTimeOffset(JsonElement element, DateTimeOffset fallback, params string[] path)
    {
        var value = GetNestedString(element, path);
        return DateTimeOffset.TryParse(value, out var parsed) ? parsed : fallback;
    }

    public static DateTimeOffset? GetNestedNullableDateTimeOffset(JsonElement element, params string[] path)
    {
        var value = GetNestedString(element, path);
        return DateTimeOffset.TryParse(value, out var parsed) ? parsed : null;
    }

    private static bool ContainsIgnoredTerm(string? value)
    {
        return value?.Contains("MetricHistory", StringComparison.OrdinalIgnoreCase) == true
            || value?.Contains("Metric History", StringComparison.OrdinalIgnoreCase) == true
            || value?.Contains("Cycle", StringComparison.OrdinalIgnoreCase) == true;
    }
}
