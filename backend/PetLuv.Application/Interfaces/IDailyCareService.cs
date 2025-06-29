using PetLuv.Application.DTOs;

namespace PetLuv.Application.Interfaces;

public interface IDailyCareService
{
    Task<DailyCareDto> CreateDailyCareAsync(CreateDailyCareRequestDto createDto, int ownerId);
    Task<bool> UpdateDailyCareAsync(int careId, UpdateDailyCareRequestDto updateDto, int ownerId);
    Task<bool> DeleteDailyCareAsync(int careId, int ownerId);
    Task<DailyCareDto?> GetDailyCareAsync(int careId, int petId, int ownerId);
    Task<IEnumerable<DailyCareDto>> GetDailyCaresAsync(int petId, int ownerId);
}