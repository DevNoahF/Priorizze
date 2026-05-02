using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.UseCases;
using priorizzeProject.Integrations.Jira.Models;

namespace priorizzeProject.Integrations.Jira.Handlers;

public class JiraIssueWebhookHandler : IJiraWebhookEventHandler
{
    private readonly SyncJiraTaskUseCase _syncJiraTaskUseCase;
    private readonly ILogger<JiraIssueWebhookHandler> _logger;

    public JiraIssueWebhookHandler(
        SyncJiraTaskUseCase syncJiraTaskUseCase,
        ILogger<JiraIssueWebhookHandler> logger)
    {
        _syncJiraTaskUseCase = syncJiraTaskUseCase;
        _logger = logger;
    }

    public bool CanHandle(JiraWebhookEvent webhookEvent)
    {
        return webhookEvent.EventType.StartsWith("jira:issue_", StringComparison.OrdinalIgnoreCase)
            || webhookEvent.EventType.Equals("issue_created", StringComparison.OrdinalIgnoreCase)
            || webhookEvent.EventType.Equals("issue_updated", StringComparison.OrdinalIgnoreCase)
            || webhookEvent.EventType.Equals("issue_deleted", StringComparison.OrdinalIgnoreCase);
    }

    public async Task HandleAsync(JiraWebhookEvent webhookEvent, CancellationToken cancellationToken)
    {
        if (webhookEvent.EventType.Contains("deleted", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation("Jira issue deleted event received for {IssueKey}. Delete handling is not implemented yet.", webhookEvent.IssueKey);
            return;
        }

        var request = MapToSyncRequest(webhookEvent);

        if (string.IsNullOrWhiteSpace(request.ExternalKey))
        {
            _logger.LogWarning("Jira issue webhook ignored because issue key was missing.");
            return;
        }

        var response = await _syncJiraTaskUseCase.ExecuteAsync(request);

        if (response is null)
        {
            _logger.LogWarning("Jira issue webhook failed to sync issue {IssueKey}.", request.ExternalKey);
            return;
        }

        _logger.LogInformation("Jira issue {IssueKey} synced from webhook event {EventType}.", request.ExternalKey, webhookEvent.EventType);
    }

    private static SyncJiraTaskRequestDto MapToSyncRequest(JiraWebhookEvent webhookEvent)
    {
        var payload = webhookEvent.Payload;
        var now = DateTimeOffset.UtcNow;

        return new SyncJiraTaskRequestDto
        {
            ExternalKey = webhookEvent.IssueKey ?? string.Empty,
            Summary = JiraWebhookEvent.GetNestedString(payload, "issue", "fields", "summary") ?? string.Empty,
            ProjectId = JiraWebhookEvent.GetNestedInt(payload, 0, "issue", "fields", "project", "id"),
            SquadId = 0,
            IssueType = JiraWebhookEvent.GetNestedString(payload, "issue", "fields", "issuetype", "name") ?? string.Empty,
            StatusName = JiraWebhookEvent.GetNestedString(payload, "issue", "fields", "status", "name") ?? string.Empty,
            PriorityName = JiraWebhookEvent.GetNestedString(payload, "issue", "fields", "priority", "name") ?? string.Empty,
            StoryPoints = JiraWebhookEvent.GetNestedDouble(payload, "issue", "fields", "customfield_10016"),
            AssigneeAccountId = JiraWebhookEvent.GetNestedString(payload, "issue", "fields", "assignee", "accountId"),
            CreatedAt = JiraWebhookEvent.GetNestedDateTimeOffset(payload, now, "issue", "fields", "created"),
            UpdatedAt = JiraWebhookEvent.GetNestedDateTimeOffset(payload, now, "issue", "fields", "updated"),
            ResolvedAt = JiraWebhookEvent.GetNestedNullableDateTimeOffset(payload, "issue", "fields", "resolutiondate")
        };
    }
}
