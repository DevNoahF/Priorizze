using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using priorizzeProject.Adapter.Dtos.Requests;
using priorizzeProject.Adapter.Dtos.Responses;
using priorizzeProject.Adapter.UseCases;

namespace priorizzeProject.Adapter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OkrsController : ControllerBase
{
    private readonly CreateOkrUseCase _createOkrUseCase;

    // Injetamos o Use Case diretamente pelo construtor
    public OkrsController(CreateOkrUseCase createOkrUseCase)
    {
        _createOkrUseCase = createOkrUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOkrRequestDto request)
    {
        // Executa a regra de negócio isolada no Use Case
        var response = await _createOkrUseCase.ExecuteAsync(request);

        // Como definimos que o Use Case retorna null em caso de erro/catch
        if (response == null)
        {
            return BadRequest(new { message = "Não foi possível criar o OKR. Verifique os logs de erro." });
        }

        // Retorna 201 Created (Padrão REST para criação com sucesso)
        return StatusCode(201, response);
    }
    
}