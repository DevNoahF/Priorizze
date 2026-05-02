using priorizzeProject.Integrations.Jira.Models;

namespace priorizzeProject.Integrations.Jira.Handlers;

public class JiraCommentWebhookHandler : IJiraWebhookEventHandler
{
    private readonly ILogger<JiraCommentWebhookHandler> _logger;

    public JiraCommentWebhookHandler(ILogger<JiraCommentWebhookHandler> logger)
    {
        _logger = logger;
    }

    public bool CanHandle(JiraWebhookEvent webhookEvent)
    {
        return webhookEvent.EventType.Contains("comment", StringComparison.OrdinalIgnoreCase);
    }

    public Task HandleAsync(JiraWebhookEvent webhookEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Jira comment event {EventType} received for issue {IssueKey}.", webhookEvent.EventType, webhookEvent.IssueKey);
        return Task.CompletedTask;
    }
}
