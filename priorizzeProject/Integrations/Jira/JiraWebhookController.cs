using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("webhooks/jira")]
public class JiraWebhookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Receive()
    {
                
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        Console.WriteLine("Webhook do Jira recebido!");
        Console.WriteLine(body);
        
        return Ok();
    }
}