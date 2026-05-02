using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Integrations.Jira.Dispatching;
using priorizzeProject.Integrations.Jira.Models;

namespace priorizzeProject.Integrations.Jira;

[ApiController]
[Route("webhooks/jira")]
public class JiraWebhookController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IJiraWebhookDispatcher _dispatcher;
    private readonly ILogger<JiraWebhookController> _logger;

    public JiraWebhookController(
        IConfiguration configuration,
        IJiraWebhookDispatcher dispatcher,
        ILogger<JiraWebhookController> logger)
    {
        _configuration = configuration;
        _dispatcher = dispatcher;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Receive([FromBody] JsonElement payload, CancellationToken cancellationToken)
    {
        if (!IsAuthorized())
        {
            _logger.LogWarning("Rejected Jira webhook request due to invalid secret.");
            return Unauthorized();
        }

        var webhookEvent = JiraWebhookEvent.FromPayload(payload);
        await _dispatcher.DispatchAsync(webhookEvent, cancellationToken);

        return Ok(new
        {
            message = "Jira webhook received.",
            webhookEvent.EventType,
            webhookEvent.IssueKey
        });
    }

    private bool IsAuthorized()
    {
        var expectedSecret = _configuration["JiraWebhook:Secret"];

        if (string.IsNullOrWhiteSpace(expectedSecret))
        {
            return true;
        }

        var headerName = _configuration["JiraWebhook:SecretHeaderName"] ?? "X-Jira-Webhook-Secret";

        return Request.Headers.TryGetValue(headerName, out var receivedSecret)
            && string.Equals(receivedSecret.ToString(), expectedSecret, StringComparison.Ordinal);
    }
}
