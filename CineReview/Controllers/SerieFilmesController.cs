using Microsoft.AspNetCore.Mvc;
using CineReview.Models.DTOs;
using CineReview.Services;
using System.Collections.Generic;
using CineReview.Models.Enums;

namespace CineReview.Controllers.Api;


[ApiController]
[Route("api/[controller]")]
public class SerieFilmesController : ControllerBase
{
    private readonly ISerieFilmeService _service;

    // di 
    public SerieFilmesController(ISerieFilmeService service)
    {
        _service = service;
    }

    [HttpGet] // get all
    public ActionResult<IEnumerable<SerieFilmeResponseDTO>> GetTodos()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id:int}")] // get by id
    public ActionResult<SerieFilmeResponseDTO> GetPorId(int id)
    {
        var item = _service.GetById(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost] // create
    public ActionResult<SerieFilmeResponseDTO> Create([FromBody] SerieFilmeCreateDTO dto)
    {
        if (dto is null || !ModelState.IsValid)
            return BadRequest("Dados inválidos.");
        var novo = _service.Create(dto);
        return CreatedAtAction(nameof(GetPorId), new { id = novo.Id }, novo);
    }

    [HttpPut("{id:int}")] // update
    public IActionResult Update(int id, [FromBody] SerieFilmeUpdateDTO dto)
    {
        if (dto is null || id != dto.Id || !ModelState.IsValid)
            return BadRequest("Dados inválidos.");
        var ok = _service.Update(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")] // delete
    public IActionResult Delete(int id)
    {
        var ok = _service.Delete(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("filtro")]  // filter by  genero, tipo, minAvaliacao, maxAvaliacao
    public ActionResult<IEnumerable<SerieFilmeResponseDTO>> Filtrar([FromQuery] GeneroEnum? genero, [FromQuery] SerieFilmeEnum? tipo,
        [FromQuery] double? minAvaliacao, [FromQuery] double? maxAvaliacao)
    {
        var lista = _service.Filter(genero, tipo, minAvaliacao, maxAvaliacao);
        return Ok(lista);
    }

    [HttpGet("ranking")]
    public ActionResult<IEnumerable<SerieFilmeRankDTO>> Ranking([FromQuery] int top = 10, [FromQuery] GeneroEnum? genero = null, [FromQuery] SerieFilmeEnum? tipo = null)
    {
        var lista = _service.GetRanking(top, genero, tipo);
        return Ok(lista);
    }
}
