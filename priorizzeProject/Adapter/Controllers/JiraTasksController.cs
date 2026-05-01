using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Core.Interfaces;

namespace priorizzeProject.Adapter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JiraTasksController : ControllerBase
{
    // 2. Trocamos a classe concreta pela Interface
    private readonly ISyncJiraTaskUseCase _syncJiraTaskUseCase;

    // 3. Injetamos a Interface no construtor
    public JiraTasksController(ISyncJiraTaskUseCase syncJiraTaskUseCase)
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