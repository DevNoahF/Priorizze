using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Adapter.Dtos.Requests.JiraProjects;
using priorizzeProject.Adapter.Dtos.Responses.JiraProjects;
using priorizzeProject.Adapter.UseCases.JiraProjects;
using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JiraProjectsController : ControllerBase
{
    private readonly IJiraProjectsUseCase _jiraProjectsUseCase;

    public JiraProjectsController(IJiraProjectsUseCase jiraProjectsUseCase)
    {
        _jiraProjectsUseCase = jiraProjectsUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<JiraProjectsResponseDTO>> Create([FromBody] CreateJiraProjectRequestDTO request)
    {
        try
        {
            var jiraProject = await _jiraProjectsUseCase.ExecuteAsync(
                request.JiraId,
                request.ProjectKey,
                request.Name,
                request.JiraUrl);

            return CreatedAtAction(nameof(GetById), new { id = jiraProject.Id }, ToResponse(jiraProject));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erro ao criar projeto Jira: {ex.Message}" });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<JiraProjectsResponseDTO>> GetById([FromRoute] int id)
    {
        var jiraProject = await _jiraProjectsUseCase.GetByIdAsync(id);
        return jiraProject is null ? NotFound() : Ok(ToResponse(jiraProject));
    }

    [HttpGet]
    public async Task<ActionResult<List<JiraProjectsResponseDTO>>> GetAll()
    {
        var jiraProjects = await _jiraProjectsUseCase.GetAllAsync();
        return Ok(jiraProjects.Select(ToResponse).ToList());
    }

    [HttpGet("by-key/{projectKey}")]
    public async Task<ActionResult<JiraProjectsResponseDTO>> GetByProjectKey([FromRoute] string projectKey)
    {
        var jiraProject = await _jiraProjectsUseCase.GetByProjectKeyAsync(projectKey);
        return jiraProject is null ? NotFound() : Ok(ToResponse(jiraProject));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var success = await _jiraProjectsUseCase.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }

    private static JiraProjectsResponseDTO ToResponse(JiraProjects jiraProject)
    {
        return new JiraProjectsResponseDTO
        {
            Id = jiraProject.Id,
            JiraId = jiraProject.JiraId,
            ProjectKey = jiraProject.ProjectKey,
            Name = jiraProject.Name,
            JiraUrl = jiraProject.JiraUrl,
            LastSync = jiraProject.LastSync
        };
    }
}

