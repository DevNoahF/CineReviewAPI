using Microsoft.AspNetCore.Mvc;
using CineReview.Services;
using CineReview.Models.DTOs;
using CineReview.Models.Enums;

namespace CineReview.Controllers.Api;

/// <summary>
/// API para operações CRUD e consulta de séries e filmes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SerieFilmesApiController : ControllerBase
{
    private readonly ISerieFilmeService _service;
    public SerieFilmesApiController(ISerieFilmeService service) => _service = service;

    /// <summary>Lista todos ou aplica filtros.</summary>
    /// <param name="genero">Filtro por gênero.</param>
    /// <param name="tipo">Filtro por tipo (Série/Filme).</param>
    /// <param name="minAvaliacao">Avaliação mínima.</param>
    /// <param name="maxAvaliacao">Avaliação máxima.</param>
    /// <param name="search">Texto a buscar em título/descrição.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SerieFilmeDto>), 200)]
    public async Task<IActionResult> Get([FromQuery] GeneroEnum? genero, [FromQuery] SerieFilmeEnum? tipo,
        [FromQuery] double? minAvaliacao, [FromQuery] double? maxAvaliacao, [FromQuery] string? search)
    {
        if (minAvaliacao.HasValue && maxAvaliacao.HasValue && minAvaliacao > maxAvaliacao)
        {
            (minAvaliacao, maxAvaliacao) = (maxAvaliacao, minAvaliacao);
        }
        var dados = (genero.HasValue || tipo.HasValue || minAvaliacao.HasValue || maxAvaliacao.HasValue || !string.IsNullOrWhiteSpace(search))
            ? await _service.FilterAsync(genero, tipo, minAvaliacao, maxAvaliacao, search)
            : await _service.GetAllAsync();
        return Ok(dados);
    }

    /// <summary>Obtém pelo Id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SerieFilmeDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Cria um novo registro.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(SerieFilmeDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] SerieFilmeCreateDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var id = await _service.CreateAsync(dto);
        var criado = await _service.GetByIdAsync(id);
        return CreatedAtAction(nameof(GetById), new { id }, criado);
    }

    /// <summary>Atualiza um registro existente.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] SerieFilmeUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest("Id do corpo difere do parâmetro.");
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var ok = await _service.UpdateAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    /// <summary>Remove um registro.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    /// <summary>Ranking Top N por avaliação.</summary>
    [HttpGet("ranking")]
    [ProducesResponseType(typeof(IEnumerable<SerieFilmeRankDto>), 200)]
    public async Task<IActionResult> Ranking([FromQuery] int top = 10, [FromQuery] GeneroEnum? genero = null, [FromQuery] SerieFilmeEnum? tipo = null)
    {
        if (top < 1) top = 1;
        var lista = await _service.GetRankingAsync(top, genero, tipo);
        return Ok(lista);
    }
}

