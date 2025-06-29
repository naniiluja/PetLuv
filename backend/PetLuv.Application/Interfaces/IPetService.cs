using PetLuv.Application.DTOs;

namespace PetLuv.Application.Interfaces;

public interface IPetService
{
    Task<IEnumerable<PetDto>> GetPetsByOwnerAsync(int ownerId);
    Task<PetDto?> GetPetAsync(int id, int ownerId);
    Task<PetDto> CreatePetAsync(CreatePetRequestDto createDto, int ownerId);
    Task<bool> UpdatePetAsync(int id, UpdatePetRequestDto updateDto, int ownerId);
    Task<bool> DeletePetAsync(int id, int ownerId);
}