using Microsoft.AspNetCore.Mvc;
using CineReview.Services;
using CineReview.Models.DTOs;

namespace CineReview.Controllers;

public class SerieFilmesController : Controller
{
    private readonly ISerieFilmeService _service;

    public SerieFilmesController(ISerieFilmeService service)
    {
        _service = service;
    }

    // GET: SerieFilmes
    public async Task<IActionResult> Index()
    {
        var lista = await _service.GetAllAsync();
        return View(lista);
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
            ImagemURL = item.ImagemURL
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
