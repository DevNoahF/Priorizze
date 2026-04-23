using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Adapter.UseCases;
using priorizzeProject.Core.Models;
using priorizzeProject.Core.Interfaces;

namespace priorizzeProject.Adapter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserUseCase _userUseCase;

    public UsersController(IUserUseCase userUseCase)
    {
        _userUseCase = userUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDTO>> Create([FromBody] CreateUserRequestDTO request)
    {
        var user = await _userUseCase.ExecuteAsync(
            request.Name,
            request.Email,
            request.Role,
            request.JiraAccountId,
            request.JiraEmail,
            request.JiraApiTokenEnc,
            request.SquadName,
            request.SquadJiraKey,
            request.JiraTeamId);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, ToResponse(user));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponseDTO>> GetById([FromRoute] Guid id)
    {
        var user = await _userUseCase.GetByIdAsync(id);
        return user is null ? NotFound() : Ok(ToResponse(user));
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponseDTO>>> GetAll()
    {
        var users = await _userUseCase.GetAllAsync();
        return Ok(users.Select(ToResponse).ToList());
    }

    private static UserResponseDTO ToResponse(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Role = user.Role,
        SquadName = user.SquadName,
        SquadJiraKey = user.SquadJiraKey,
        JiraTeamId = user.JiraTeamId,
        JiraAccountId = user.JiraAccountId,
        JiraEmail = user.JiraEmail,
        CreatedAt = user.CreatedAt
    };
}
