using PetLuv.Domain.Entities;

namespace PetLuv.Application.Interfaces;

public interface IDailyCareRepository
{
    Task<DailyCare?> GetByIdAsync(int id);
    Task<IEnumerable<DailyCare>> GetByPetIdAsync(int petId);
    Task<DailyCare> AddAsync(DailyCare dailyCare);
    Task<bool> UpdateAsync(DailyCare dailyCare);
    Task<bool> DeleteAsync(int id);
}