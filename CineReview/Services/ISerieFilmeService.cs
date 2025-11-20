using CineReview.Models.DTOs;

namespace CineReview.Services;

public interface ISerieFilmeService
{
    Task<List<SerieFilmeDto>> GetAllAsync();
    Task<SerieFilmeDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(SerieFilmeCreateDto dto);
    Task<bool> UpdateAsync(int id, SerieFilmeUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

