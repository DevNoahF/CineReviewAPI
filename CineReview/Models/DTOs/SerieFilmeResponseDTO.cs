using System.ComponentModel.DataAnnotations;
using CineReview.Models.Enums;

namespace CineReview.Models.DTOs;

public class SerieFilmeResponseDTO
{
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public string Comentario { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public GeneroEnum Genero { get; set; }

    [Range(1, int.MaxValue)]
    public SerieFilmeEnum Tipo { get; set; }

    [Required]
    [Url]
    [StringLength(500)]
    public string ImagemURL { get; set; } = string.Empty;

    [Range(0,10)]
    public double Nota { get; set; }
}
