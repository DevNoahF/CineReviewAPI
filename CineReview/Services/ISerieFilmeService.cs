using CineReview.Models.DTOs;
using CineReview.Models.Enums;

namespace CineReview.Services;

public interface ISerieFilmeService
{
    IEnumerable<SerieFilmeDto> GetAll();
    SerieFilmeDto? GetById(int id);
    SerieFilmeDto Create(SerieFilmeCreateDto dto);
    bool Update(int id, SerieFilmeUpdateDto dto);
    bool Delete(int id);
    IEnumerable<SerieFilmeDto> Filter(GeneroEnum? genero, SerieFilmeEnum? tipo, double? minAvaliacao, double? maxAvaliacao);
    IEnumerable<SerieFilmeRankDto> GetRanking(int top = 10, GeneroEnum? genero = null, SerieFilmeEnum? tipo = null);
}
