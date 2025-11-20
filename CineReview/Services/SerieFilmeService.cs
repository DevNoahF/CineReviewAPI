using CineReview.Data;
using CineReview.Models;
using CineReview.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CineReview.Services;

public class SerieFilmeService : ISerieFilmeService
{
    private readonly ApplicationDbContext _db;

    public SerieFilmeService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<SerieFilmeDto>> GetAllAsync()
    {
        return await _db.SerieFilmes
            .AsNoTracking()
            .Select(e => new SerieFilmeDto
            {
                Id = e.Id,
                Titulo = e.Titulo,
                Descricao = e.Descricao,
                Genero = e.Genero,
                Tipo = e.Tipo,
                ImagemURL = e.ImagemURL
            })
            .ToListAsync();
    }

    public async Task<SerieFilmeDto?> GetByIdAsync(int id)
    {
        return await _db.SerieFilmes
            .AsNoTracking()
            .Where(e => e.Id == id)
            .Select(e => new SerieFilmeDto
            {
                Id = e.Id,
                Titulo = e.Titulo,
                Descricao = e.Descricao,
                Genero = e.Genero,
                Tipo = e.Tipo,
                ImagemURL = e.ImagemURL
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(SerieFilmeCreateDto dto)
    {
        var entity = new SerieFilmeModel
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Genero = dto.Genero,
            Tipo = dto.Tipo,
            ImagemURL = dto.ImagemURL
        };
        _db.SerieFilmes.Add(entity);
        await _db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(int id, SerieFilmeUpdateDto dto)
    {
        var entity = await _db.SerieFilmes.FindAsync(id);
        if (entity == null) return false;

        entity.Titulo = dto.Titulo;
        entity.Descricao = dto.Descricao;
        entity.Genero = dto.Genero;
        entity.Tipo = dto.Tipo;
        entity.ImagemURL = dto.ImagemURL;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.SerieFilmes.FindAsync(id);
        if (entity == null) return false;
        _db.SerieFilmes.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}

