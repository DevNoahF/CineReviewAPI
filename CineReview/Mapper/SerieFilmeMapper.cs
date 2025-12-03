using CineReview.Models;
using CineReview.Models.DTOs;

namespace CineReview.Mapper;

public static class SerieFilmeMapper
{
    // Model -> Response DTO
    public static SerieFilmeResponseDTO ToDto(SerieFilmeModel model) => new()
    {
        Id = model.Id,
        Titulo = model.Titulo,
        Comentario = model.Comentario,
        Genero = model.Genero,
        Tipo = model.Tipo,
        ImagemURL = model.ImagemURL,
        Nota = model.Nota
    };

    // Create DTO -> Model
    public static SerieFilmeModel FromCreateDto(SerieFilmeCreateDTO dto) => new()
    {
        Titulo = dto.Titulo,
        Comentario = dto.Comentario,
        Genero = dto.Genero,
        Tipo = dto.Tipo,
        ImagemURL = dto.ImagemURL,
        Nota = dto.Nota
    };

    // Update DTO -> apply onto existing model
    public static void ApplyUpdate(SerieFilmeModel model, SerieFilmeUpdateDTO dto)
    {
        model.Titulo = dto.Titulo;
        model.Comentario = dto.Comentario;
        model.Genero = dto.Genero;
        model.Tipo = dto.Tipo;
        model.ImagemURL = dto.ImagemURL;
        model.Nota = dto.Nota;
    }

}