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

    public IEnumerable<SerieFilmeDto> GetAll()
    {
        return _db.SerieFilmes
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(ToDto)
            .ToList();
    }

    public SerieFilmeDto? GetById(int id)
    {
        return _db.SerieFilmes
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(ToDto)
            .FirstOrDefault();
    }

    public SerieFilmeDto Create(SerieFilmeCreateDto dto)
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
        _db.SaveChanges();
        return ToDto(entity);
    }

    public bool Update(int id, SerieFilmeUpdateDto dto)
    {
        var entity = _db.SerieFilmes.Find(id);
        if (entity is null) return false;

        entity.Titulo = dto.Titulo;
        entity.Descricao = dto.Descricao;
        entity.Genero = dto.Genero;
        entity.Tipo = dto.Tipo;
        entity.ImagemURL = dto.ImagemURL;
        entity.Avaliacao = dto.Avaliacao;
        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var entity = _db.SerieFilmes.Find(id);
        if (entity is null) return false;
        _db.SerieFilmes.Remove(entity);
        _db.SaveChanges();
        return true;
    }

    public IEnumerable<SerieFilmeDto> Filter(GeneroEnum? genero, SerieFilmeEnum? tipo, double? minAvaliacao, double? maxAvaliacao)
    {
        var query = _db.SerieFilmes.AsNoTracking().AsQueryable();
        if (genero.HasValue)
            query = query.Where(x => x.Genero == genero.Value);
        if (tipo.HasValue)
            query = query.Where(x => x.Tipo == tipo.Value);
        if (minAvaliacao.HasValue)
            query = query.Where(x => x.Avaliacao >= minAvaliacao.Value);
        if (maxAvaliacao.HasValue)
            query = query.Where(x => x.Avaliacao <= maxAvaliacao.Value);

        return query
            .OrderByDescending(x => x.Avaliacao)
            .ThenBy(x => x.Titulo)
            .Select(ToDto)
            .ToList();
    }

    public IEnumerable<SerieFilmeRankDto> GetRanking(int top = 10, GeneroEnum? genero = null, SerieFilmeEnum? tipo = null)
    {
        var lista = Filter(genero, tipo, null, null)
            .Take(Math.Max(top, 1))
            .ToList();

        var posicao = 1;
        return lista.Select(item => new SerieFilmeRankDto
        {
            Posicao = posicao++,
            Id = item.Id,
            Titulo = item.Titulo,
            Genero = item.Genero,
            Tipo = item.Tipo,
            Avaliacao = item.Avaliacao,
            ImagemURL = item.ImagemURL
        }).ToList();
    }

    private static SerieFilmeDto ToDto(SerieFilmeModel model) => new()
    {
        Id = model.Id,
        Titulo = model.Titulo,
        Descricao = model.Descricao,
        Genero = model.Genero,
        Tipo = model.Tipo,
        ImagemURL = model.ImagemURL,
        Avaliacao = model.Avaliacao
    };
}
