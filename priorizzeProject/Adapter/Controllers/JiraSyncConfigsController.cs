using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Adapter.UseCases;
using priorizzeProject.Core.Models;
using priorizzeProject.Core.Interfaces;

namespace priorizzeProject.Adapter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JiraSyncConfigsController : ControllerBase
{
    private readonly IJiraSyncConfigUseCase _jiraSyncConfigUseCase;
    private readonly IJiraProjectSyncUseCase _jiraProjectSyncUseCase;
    private readonly IUserUseCase _userUseCase;

    public JiraSyncConfigsController(
        IJiraSyncConfigUseCase jiraSyncConfigUseCase,
        IJiraProjectSyncUseCase jiraProjectSyncUseCase,
        IUserUseCase userUseCase)
    {
        _jiraSyncConfigUseCase = jiraSyncConfigUseCase;
        _jiraProjectSyncUseCase = jiraProjectSyncUseCase;
        _userUseCase = userUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<JiraSyncConfigResponseDTO>> Create([FromBody] CreateJiraSyncConfigRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (request.UserId == Guid.Empty)
        {
            return BadRequest("UserId is required.");
        }

        if (string.IsNullOrWhiteSpace(request.ProjectKey))
        {
            return BadRequest("ProjectKey is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Url))
        {
            return BadRequest("Url is required.");
        }

        var user = await _userUseCase.GetByIdAsync(request.UserId);
        if (user is null)
        {
            return NotFound($"User '{request.UserId}' was not found.");
        }

        var syncConfig = await _jiraSyncConfigUseCase.ExecuteAsync(
            request.UserId,
            request.ProjectKey,
            request.Url);

        return CreatedAtAction(nameof(GetById), new { id = syncConfig.Id }, ToResponse(syncConfig));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JiraSyncConfigResponseDTO>> GetById([FromRoute] Guid id)
    {
        var syncConfig = await _jiraSyncConfigUseCase.GetByIdAsync(id);
        return syncConfig is null ? NotFound() : Ok(ToResponse(syncConfig));
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<List<JiraSyncConfigResponseDTO>>> GetByUserId([FromRoute] Guid userId)
    {
        var syncConfigs = await _jiraSyncConfigUseCase.GetByUserIdAsync(userId);
        return Ok(syncConfigs.Select(ToResponse).ToList());
    }

    [HttpPatch("{id:guid}/sync")]
    public async Task<IActionResult> MarkSynced([FromRoute] Guid id, [FromBody] MarkSyncedRequestDTO request)
    {
        var updated = await _jiraSyncConfigUseCase.MarkSyncedAsync(id, request.SyncTime);
        return updated ? NoContent() : NotFound();
    }

    [HttpPost("{id:guid}/projects/sync")]
    public async Task<ActionResult<JiraProjectSyncResponseDTO>> SyncProject([FromRoute] Guid id)
    {
        try
        {
            var jiraProject = await _jiraProjectSyncUseCase.ExecuteAsync(id);
            return Ok(ToResponse(jiraProject));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
        }
    }

    private static JiraSyncConfigResponseDTO ToResponse(JiraSyncConfig syncConfig) => new()
    {
        Id = syncConfig.Id,
        UserId = syncConfig.UserId,
        ProjectKey = syncConfig.ProjectKey,
        Url = syncConfig.Url,
        LastSync = syncConfig.LastSync
    };

    private static JiraProjectSyncResponseDTO ToResponse(JiraProjects jiraProject) => new()
    {
        Id = jiraProject.Id,
        JiraId = jiraProject.JiraId,
        ProjectKey = jiraProject.ProjectKey,
        Name = jiraProject.Name,
        JiraUrl = jiraProject.JiraUrl,
        LastSync = jiraProject.LastSync
    };
}
