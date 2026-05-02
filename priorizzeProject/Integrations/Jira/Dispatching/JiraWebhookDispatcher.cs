using priorizzeProject.Integrations.Jira.Handlers;
using priorizzeProject.Integrations.Jira.Models;

namespace priorizzeProject.Integrations.Jira.Dispatching;

public class JiraWebhookDispatcher : IJiraWebhookDispatcher
{
    private readonly IEnumerable<IJiraWebhookEventHandler> _handlers;
    private readonly ILogger<JiraWebhookDispatcher> _logger;

    public JiraWebhookDispatcher(
        IEnumerable<IJiraWebhookEventHandler> handlers,
        ILogger<JiraWebhookDispatcher> logger)
    {
        _handlers = handlers;
        _logger = logger;
    }

    public async Task DispatchAsync(JiraWebhookEvent webhookEvent, CancellationToken cancellationToken)
    {
        if (webhookEvent.IsMetricHistoryOrCycleEvent())
        {
            _logger.LogInformation("Ignored Jira webhook event {EventType} for issue {IssueKey}.", webhookEvent.EventType, webhookEvent.IssueKey);
            return;
        }

        var handler = _handlers.FirstOrDefault(handler => handler.CanHandle(webhookEvent));

        if (handler is null)
        {
            _logger.LogInformation("No Jira webhook handler registered for event {EventType}.", webhookEvent.EventType);
            return;
        }

        await handler.HandleAsync(webhookEvent, cancellationToken);
    }
}
