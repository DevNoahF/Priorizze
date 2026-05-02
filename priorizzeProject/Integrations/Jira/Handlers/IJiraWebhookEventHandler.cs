using priorizzeProject.Integrations.Jira.Models;

namespace priorizzeProject.Integrations.Jira.Handlers;

public interface IJiraWebhookEventHandler
{
    bool CanHandle(JiraWebhookEvent webhookEvent);

    Task HandleAsync(JiraWebhookEvent webhookEvent, CancellationToken cancellationToken);
}
