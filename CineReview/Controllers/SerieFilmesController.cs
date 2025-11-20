using Microsoft.AspNetCore.Mvc;
using CineReview.Services;
using CineReview.Models.DTOs;
using CineReview.Models.Enums;

namespace CineReview.Controllers;

public class SerieFilmesController : Controller
{
    private readonly ISerieFilmeService _service;

    public SerieFilmesController(ISerieFilmeService service)
    {
        _service = service;
    }

    // GET: SerieFilmes (with optional filters)
    public async Task<IActionResult> Index(GeneroEnum? genero, SerieFilmeEnum? tipo, double? minAvaliacao, double? maxAvaliacao, string? search)
    {
        if (minAvaliacao.HasValue && maxAvaliacao.HasValue && minAvaliacao > maxAvaliacao)
        {
            var temp = minAvaliacao; minAvaliacao = maxAvaliacao; maxAvaliacao = temp; // swap to maintain logical order
        }
        var lista = (genero.HasValue || tipo.HasValue || minAvaliacao.HasValue || maxAvaliacao.HasValue || !string.IsNullOrWhiteSpace(search))
            ? await _service.FilterAsync(genero, tipo, minAvaliacao, maxAvaliacao, search)
            : await _service.GetAllAsync();

        ViewBag.Genero = genero;
        ViewBag.Tipo = tipo;
        ViewBag.MinAvaliacao = minAvaliacao;
        ViewBag.MaxAvaliacao = maxAvaliacao;
        ViewBag.Search = search;
        return View(lista);
    }

    // GET: SerieFilmes/Ranking
    public async Task<IActionResult> Ranking(int top = 10, GeneroEnum? genero = null, SerieFilmeEnum? tipo = null)
    {
        if (top < 1) top = 1; // garante mínimo de 1
        var ranking = await _service.GetRankingAsync(top, genero, tipo);
        ViewBag.Top = top;
        ViewBag.Genero = genero;
        ViewBag.Tipo = tipo;
        return View(ranking);
    }

    // GET: SerieFilmes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var item = await _service.GetByIdAsync(id.Value);
        if (item == null) return NotFound();
        return View(item);
    }

    // GET: SerieFilmes/Create
    public IActionResult Create()
    {
        return View(new SerieFilmeCreateDto());
    }

    // POST: SerieFilmes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SerieFilmeCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _service.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    // GET: SerieFilmes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = await _service.GetByIdAsync(id.Value);
        if (item == null) return NotFound();
        var model = new SerieFilmeUpdateDto
        {
            Id = item.Id,
            Titulo = item.Titulo,
            Descricao = item.Descricao,
            Genero = item.Genero,
            Tipo = item.Tipo,
            ImagemURL = item.ImagemURL,
            Avaliacao = item.Avaliacao
        };
        return View(model);
    }

    // POST: SerieFilmes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SerieFilmeUpdateDto dto)
    {
        if (id != dto.Id) return NotFound();
        if (!ModelState.IsValid) return View(dto);
        var ok = await _service.UpdateAsync(id, dto);
        if (!ok) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    // GET: SerieFilmes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var item = await _service.GetByIdAsync(id.Value);
        if (item == null) return NotFound();
        return View(item);
    }

    // POST: SerieFilmes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
