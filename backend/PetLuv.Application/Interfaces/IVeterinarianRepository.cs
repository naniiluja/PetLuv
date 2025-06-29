using PetLuv.Domain.Entities;

namespace PetLuv.Application.Interfaces;

public interface IVeterinarianRepository
{
    Task<Veterinarian?> GetByIdAsync(int id);
    Task<Veterinarian?> GetByUserIdAsync(int userId);
    Task<Veterinarian> AddAsync(Veterinarian veterinarian);
    Task<bool> UpdateAsync(Veterinarian veterinarian);
    Task<IEnumerable<Veterinarian>> SearchAsync(string? specialty, string? location); // For future search feature
}