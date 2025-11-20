using CineReview.Data;
using CineReview.Models;
using CineReview.Models.DTOs;
using CineReview.Models.Enums;
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
                ImagemURL = e.ImagemURL,
                Avaliacao = e.Avaliacao
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
                ImagemURL = e.ImagemURL,
                Avaliacao = e.Avaliacao
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
            ImagemURL = dto.ImagemURL,
            Avaliacao = dto.Avaliacao
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
        entity.Avaliacao = dto.Avaliacao;

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

    public async Task<List<SerieFilmeDto>> FilterAsync(GeneroEnum? genero, SerieFilmeEnum? tipo, double? minAvaliacao, double? maxAvaliacao, string? search)
    {
        var query = _db.SerieFilmes.AsNoTracking().AsQueryable();

        if (genero.HasValue)
            query = query.Where(e => e.Genero == genero.Value);
        if (tipo.HasValue)
            query = query.Where(e => e.Tipo == tipo.Value);
        if (minAvaliacao.HasValue)
            query = query.Where(e => e.Avaliacao >= minAvaliacao.Value);
        if (maxAvaliacao.HasValue)
            query = query.Where(e => e.Avaliacao <= maxAvaliacao.Value);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            query = query.Where(e => e.Titulo.Contains(s) || e.Descricao.Contains(s));
        }

        return await query
            .OrderByDescending(e => e.Avaliacao)
            .ThenBy(e => e.Titulo)
            .Select(e => new SerieFilmeDto
            {
                Id = e.Id,
                Titulo = e.Titulo,
                Descricao = e.Descricao,
                Genero = e.Genero,
                Tipo = e.Tipo,
                ImagemURL = e.ImagemURL,
                Avaliacao = e.Avaliacao
            }).ToListAsync();
    }

    public async Task<List<SerieFilmeRankDto>> GetRankingAsync(int top = 10, GeneroEnum? genero = null, SerieFilmeEnum? tipo = null)
    {
        var query = _db.SerieFilmes.AsNoTracking().AsQueryable();
        if (genero.HasValue)
            query = query.Where(e => e.Genero == genero.Value);
        if (tipo.HasValue)
            query = query.Where(e => e.Tipo == tipo.Value);

        var ordered = await query
            .OrderByDescending(e => e.Avaliacao)
            .ThenBy(e => e.Titulo)
            .Take(top)
            .Select(e => new { e.Id, e.Titulo, e.Genero, e.Tipo, e.Avaliacao, e.ImagemURL })
            .ToListAsync();

        var rankList = ordered
            .Select((e, idx) => new SerieFilmeRankDto
            {
                Posicao = idx + 1,
                Id = e.Id,
                Titulo = e.Titulo,
                Genero = e.Genero,
                Tipo = e.Tipo,
                Avaliacao = e.Avaliacao,
                ImagemURL = e.ImagemURL
            }).ToList();
        return rankList;
    }
}
