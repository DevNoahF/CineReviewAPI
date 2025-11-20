using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineReview.Data;
using CineReview.Models;
using CineReview.Models.Enums;

namespace CineReview.Controllers;

public class SerieFilmesController : Controller
{
    private readonly ApplicationDbContext _context;

    public SerieFilmesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: SerieFilmes
    public async Task<IActionResult> Index()
    {
        var lista = await _context.SerieFilmes.ToListAsync();
        return View(lista);
    }

    // GET: SerieFilmes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.SerieFilmes.FirstOrDefaultAsync(m => m.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    // GET: SerieFilmes/Create
    public IActionResult Create()
    {
        return View(new SerieFilmeModel());
    }

    // POST: SerieFilmes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SerieFilmeModel model)
    {
        if (!ModelState.IsValid) return View(model);
        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: SerieFilmes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.SerieFilmes.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    // POST: SerieFilmes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SerieFilmeModel model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid) return View(model);
        try
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.SerieFilmes.AnyAsync(e => e.Id == model.Id)) return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: SerieFilmes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.SerieFilmes.FirstOrDefaultAsync(m => m.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    // POST: SerieFilmes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.SerieFilmes.FindAsync(id);
        if (item != null)
        {
            _context.SerieFilmes.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}

