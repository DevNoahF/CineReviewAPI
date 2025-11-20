using CineReview.Models.Enums;

namespace CineReview.Models.DTOs;

public class SerieFilmeRankDto
{
    public int Posicao { get; set; }
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public GeneroEnum Genero { get; set; }
    public SerieFilmeEnum Tipo { get; set; }
    public double Avaliacao { get; set; }
    public string ImagemURL { get; set; } = string.Empty;
}

