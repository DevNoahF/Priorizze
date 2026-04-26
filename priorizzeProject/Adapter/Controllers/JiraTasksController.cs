using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.UseCases;

namespace priorizzeProject.Adapter.Controllers;


[ApiController]
[Route("api/[controller]")]
public class JiraTasksController : ControllerBase
{
    private readonly SyncJiraTaskUseCase _syncJiraTaskUseCase;

    public JiraTasksController(SyncJiraTaskUseCase syncJiraTaskUseCase)
    {
        _syncJiraTaskUseCase = syncJiraTaskUseCase;
    }

    [HttpPost("sync")]
    public async Task<IActionResult> Sync([FromBody] SyncJiraTaskRequestDto request)
    {
        var response = await _syncJiraTaskUseCase.ExecuteAsync(request);

        if (response == null)
        {
            return BadRequest(new { message = "Erro ao sincronizar tarefa do Jira. Verifique os logs." });
        }

        return Ok(response);
    }
}
