using CineReview.Models.DTOs;
using CineReview.Models.Enums;

namespace CineReview.Services;

public interface ISerieFilmeService
{
    IEnumerable<SerieFilmeResponseDTO> GetAll();
    SerieFilmeResponseDTO? GetById(int id);
    SerieFilmeResponseDTO Create(SerieFilmeCreateDTO dto);
    bool Update(int id, SerieFilmeUpdateDTO dto);
    bool Delete(int id);
    IEnumerable<SerieFilmeResponseDTO> Filter(GeneroEnum? genero, SerieFilmeEnum? tipo, double? minAvaliacao, double? maxAvaliacao);
    IEnumerable<SerieFilmeRankDTO> GetRanking(int top = 10, GeneroEnum? genero = null, SerieFilmeEnum? tipo = null);
}
