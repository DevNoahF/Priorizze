using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Adapter.Dtos.Requests.Cycle;
using priorizzeProject.Adapter.Dtos.Responses.Cycle;
using priorizzeProject.Adapter.UseCases.Cycle;
using priorizzeProject.Core.Interfaces;
using priorizzeProject.Core.Models;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Adapter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CyclesController : ControllerBase
{
    private readonly ICycleUseCase _cycleUseCase;

    public CyclesController(ICycleUseCase cycleUseCase)
    {
        _cycleUseCase = cycleUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<CycleResponseDTO>> Create([FromBody] CreateCycleRequestDTO request)
    {
        try
        {
            var cycle = await _cycleUseCase.ExecuteAsync(
                request.CyclesEnum,
                request.Year);

            return CreatedAtAction(nameof(GetById), new { id = cycle.Id }, ToResponse(cycle));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erro ao criar ciclo: {ex.Message}" });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CycleResponseDTO>> GetById([FromRoute] int id)
    {
        var cycle = await _cycleUseCase.GetByIdAsync(id);
        return cycle is null ? NotFound() : Ok(ToResponse(cycle));
    }

    [HttpGet]
    public async Task<ActionResult<List<CycleResponseDTO>>> GetAll()
    {
        var cycles = await _cycleUseCase.GetAllAsync();
        return Ok(cycles.Select(ToResponse).ToList());
    }

    [HttpGet("query")]
    public async Task<ActionResult<List<CycleResponseDTO>>> Query(
        [FromQuery] CyclesEnum? cyclesEnum,
        [FromQuery] int? year,
        [FromQuery] int? yearFrom,
        [FromQuery] int? yearTo)
    {
        if (year.HasValue && (yearFrom.HasValue || yearTo.HasValue))
            return BadRequest(new { message = "Use year ou yearFrom/yearTo, nao ambos." });

        if (yearFrom.HasValue && yearTo.HasValue && yearFrom > yearTo)
            return BadRequest(new { message = "yearFrom nao pode ser maior que yearTo." });

        var cycles = await _cycleUseCase.QueryAsync(cyclesEnum, year, yearFrom, yearTo);
        return Ok(cycles.Select(ToResponse).ToList());
    }

    [HttpGet("by-cycle-year/{cyclesEnum}/{year:int}")]
    public async Task<ActionResult<CycleResponseDTO>> GetByCycleAndYear(
        [FromRoute] CyclesEnum cyclesEnum,
        [FromRoute] int year)
    {
        var cycle = await _cycleUseCase.GetByCycleAndYearAsync(cyclesEnum, year);
        return cycle is null ? NotFound() : Ok(ToResponse(cycle));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var success = await _cycleUseCase.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }

    private static CycleResponseDTO ToResponse(Cycle cycle)
    {
        return new CycleResponseDTO
        {
            Id = cycle.Id,
            CyclesEnum = cycle.CyclesEnum,
            Year = cycle.Year
        };
    }
}
