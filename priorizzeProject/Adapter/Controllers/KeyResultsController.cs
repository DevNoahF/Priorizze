using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Adapter.UseCases;
using priorizzeProject.Core.Models;
using priorizzeProject.Core.Interfaces;

namespace priorizzeProject.Adapter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeyResultsController : ControllerBase
{
    private readonly IKeyResultUseCase _keyResultUseCase;

    public KeyResultsController(IKeyResultUseCase keyResultUseCase)
    {
        _keyResultUseCase = keyResultUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<KeyResultResponseDTO>> Create([FromBody] CreateKeyResultRequestDTO request)
    {
        var keyResult = await _keyResultUseCase.ExecuteAsync(
            request.OkrId,
            request.Title,
            request.InitialValue,
            request.GoalValue,
            request.CurrentValue,
            request.Unit,
            request.LimitDate,
            request.Description);

        return CreatedAtAction(nameof(GetById), new { id = keyResult.Id }, ToResponse(keyResult));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<KeyResultResponseDTO>> GetById([FromRoute] Guid id)
    {
        var keyResult = await _keyResultUseCase.GetByIdAsync(id);
        return keyResult is null ? NotFound() : Ok(ToResponse(keyResult));
    }

    [HttpGet("okr/{okrId:guid}")]
    public async Task<ActionResult<List<KeyResultResponseDTO>>> GetByOkrId([FromRoute] Guid okrId)
    {
        var keyResults = await _keyResultUseCase.GetByOkrIdAsync(okrId);
        return Ok(keyResults.Select(ToResponse).ToList());
    }

    [HttpPut("{id:guid}/current-value")]
    public async Task<IActionResult> UpdateCurrentValue([FromRoute] Guid id, [FromBody] UpdateCurrentValueRequestDTO request)
    {
        var updated = await _keyResultUseCase.UpdateCurrentValueAsync(id, request.CurrentValue);
        return updated ? NoContent() : NotFound();
    }

    private static KeyResultResponseDTO ToResponse(KeyResult keyResult) => new()
    {
        Id = keyResult.Id,
        OkrId = keyResult.OkrId,
        Title = keyResult.Title,
        Description = keyResult.Description,
        InitialValue = keyResult.InitialValue,
        GoalValue = keyResult.GoalValue,
        CurrentValue = keyResult.CurrentValue,
        Unit = keyResult.Unit,
        LimitDate = keyResult.LimitDate,
        LastUpdated = keyResult.LastUpdated
    };
}
