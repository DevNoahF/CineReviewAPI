using System.ComponentModel.DataAnnotations;

namespace CineReview.Models;

using CineReview.Models.Enums;

public class SerieFilmeModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Selecione um gênero válido.")]
    public GeneroEnum Genero { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione se é Série ou Filme.")]
    public SerieFilmeEnum Tipo { get; set; }
    
    [Required(ErrorMessage = "Informe a URL da imagem.")]
    [Url(ErrorMessage = "URL inválida.")]
    [StringLength(500, ErrorMessage = "A URL deve ter até 500 caracteres.")]
    public string ImagemURL { get; set; } = string.Empty;
}