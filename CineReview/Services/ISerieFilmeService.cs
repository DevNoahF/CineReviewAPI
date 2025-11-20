using CineReview.Models.DTOs;
using CineReview.Models.Enums;

namespace CineReview.Services;

public interface ISerieFilmeService
{
    Task<List<SerieFilmeDto>> GetAllAsync();
    Task<SerieFilmeDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(SerieFilmeCreateDto dto);
    Task<bool> UpdateAsync(int id, SerieFilmeUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<SerieFilmeDto>> FilterAsync(GeneroEnum? genero, SerieFilmeEnum? tipo, double? minAvaliacao, double? maxAvaliacao, string? search);
    Task<List<SerieFilmeRankDto>> GetRankingAsync(int top = 10, GeneroEnum? genero = null, SerieFilmeEnum? tipo = null);
}
