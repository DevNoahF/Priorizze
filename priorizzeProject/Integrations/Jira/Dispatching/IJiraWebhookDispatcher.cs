using priorizzeProject.Integrations.Jira.Models;

namespace priorizzeProject.Integrations.Jira.Dispatching;

public interface IJiraWebhookDispatcher
{
    Task DispatchAsync(JiraWebhookEvent webhookEvent, CancellationToken cancellationToken);
}
