using System.Linq;
using CineReview.Data;
using CineReview.Models.DTOs;
using CineReview.Models.Enums;
using Microsoft.EntityFrameworkCore;
using CineReview.Mapper;

namespace CineReview.Services;

public class SerieFilmeService : ISerieFilmeService
{
    private readonly ApplicationDbContext _db;

    // DI
    public SerieFilmeService(ApplicationDbContext db)
    {
        _db = db;
    }

    // get all
    public IEnumerable<SerieFilmeResponseDTO> GetAll()
    {
        return _db.SerieFilmes
            .AsNoTracking() // no tracking just read
            .OrderBy(x => x.Id)
            .AsEnumerable()
            .Select(SerieFilmeMapper.ToDto)
            .ToList();
    }

    // get by id
    public SerieFilmeResponseDTO? GetById(int id)
    {
        return _db.SerieFilmes
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(SerieFilmeMapper.ToDto)
            .FirstOrDefault();
    }

    // create
    public SerieFilmeResponseDTO Create(SerieFilmeCreateDTO dto)
    {
        var entity = SerieFilmeMapper.FromCreateDto(dto);
        _db.SerieFilmes.Add(entity);
        _db.SaveChanges();
        return SerieFilmeMapper.ToDto(entity);
    }

    // update
    public bool Update(int id, SerieFilmeUpdateDTO dto)
    {
        var serchId = _db.SerieFilmes.Find(id);
        if (serchId == null) return false;
        SerieFilmeMapper.ApplyUpdate(serchId, dto);
        _db.SaveChanges();
        return true;
    }

    // delete
    public bool Delete(int id)
    {
        var entity = _db.SerieFilmes.Find(id);
        if (entity is null) return false;
        _db.SerieFilmes.Remove(entity);
        _db.SaveChanges();
        return true;
    }

    // filter by genero, tipo, minNota, maxNota
    public IEnumerable<SerieFilmeResponseDTO> Filter(GeneroEnum? genero, SerieFilmeEnum? tipo, double? minAvaliacao, double? maxAvaliacao)
    {
        var query = _db.SerieFilmes.AsNoTracking().AsQueryable();
        if (genero.HasValue)
            query = query.Where(x => x.Genero == genero.Value);
        if (tipo.HasValue)
            query = query.Where(x => x.Tipo == tipo.Value);
        if (minAvaliacao.HasValue)
            query = query.Where(x => x.Nota >= minAvaliacao.Value);
        if (maxAvaliacao.HasValue)
            query = query.Where(x => x.Nota <= maxAvaliacao.Value);

        return query
            .OrderByDescending(x => x.Nota)
            .ThenBy(x => x.Titulo)
            .Select(SerieFilmeMapper.ToDto)
            .ToList();
    }

    // get ranking
    public IEnumerable<SerieFilmeRankDTO> GetRanking(int top = 10, GeneroEnum? genero = null, SerieFilmeEnum? tipo = null)
    {
        var lista = Filter(genero, tipo, null, null)
            .Take(Math.Max(top, 1))
            .ToList();

        var posicao = 1;
        return lista.Select(item => new SerieFilmeRankDTO
        {
            Posicao = posicao++,
            Id = item.Id,
            Titulo = item.Titulo,
            Genero = item.Genero,
            Tipo = item.Tipo,
            Avaliacao = item.Nota,
            ImagemURL = item.ImagemURL
        }).ToList();
    }
}
