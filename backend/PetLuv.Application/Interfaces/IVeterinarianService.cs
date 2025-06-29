using PetLuv.Application.DTOs;

namespace PetLuv.Application.Interfaces;

public interface IVeterinarianService
{
    Task<VeterinarianDto?> CreateProfileAsync(int userId, CreateVeterinarianProfileDto createDto);
    Task<VeterinarianDto?> GetProfileByUserIdAsync(int userId);
    Task<VeterinarianDto?> UpdateProfileAsync(int userId, UpdateVeterinarianProfileDto updateDto);
    Task<IEnumerable<VeterinarianDto>> SearchVeterinariansAsync(string? specialty, string? location);
}