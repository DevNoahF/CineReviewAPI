using System.ComponentModel.DataAnnotations;
using CineReview.Models.Enums;

namespace CineReview.Models.DTOs;

public class SerieFilmeDto
{
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public GeneroEnum Genero { get; set; }

    [Range(1, int.MaxValue)]
    public SerieFilmeEnum Tipo { get; set; }

    [Required]
    [Url]
    [StringLength(500)]
    public string ImagemURL { get; set; } = string.Empty;

    [Range(0,10)]
    public double Avaliacao { get; set; }
}
