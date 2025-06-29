using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;

namespace PetLuv.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;

    public PetService(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<PetDto> CreatePetAsync(CreatePetRequestDto createDto, int ownerId)
    {
        var pet = new Pet
        {
            Name = createDto.Name,
            Species = createDto.Species,
            Breed = createDto.Breed,
            DateOfBirth = createDto.DateOfBirth,
            OwnerId = ownerId
        };

        var createdPet = await _petRepository.AddAsync(pet);

        return new PetDto
        {
            Id = createdPet.Id,
            Name = createdPet.Name,
            Species = createdPet.Species,
            Breed = createdPet.Breed,
            DateOfBirth = createdPet.DateOfBirth,
            OwnerId = createdPet.OwnerId
        };
    }

    public async Task<bool> DeletePetAsync(int id, int ownerId)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null || pet.OwnerId != ownerId)
        {
            return false;
        }

        return await _petRepository.DeleteAsync(id);
    }

    public async Task<PetDto?> GetPetAsync(int id, int ownerId)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null || pet.OwnerId != ownerId)
        {
            return null;
        }

        return new PetDto
        {
            Id = pet.Id,
            Name = pet.Name,
            Species = pet.Species,
            Breed = pet.Breed,
            DateOfBirth = pet.DateOfBirth,
            OwnerId = pet.OwnerId
        };
    }

    public async Task<IEnumerable<PetDto>> GetPetsByOwnerAsync(int ownerId)
    {
        var pets = await _petRepository.GetByOwnerIdAsync(ownerId);
        return pets.Select(pet => new PetDto
        {
            Id = pet.Id,
            Name = pet.Name,
            Species = pet.Species,
            Breed = pet.Breed,
            DateOfBirth = pet.DateOfBirth,
            OwnerId = pet.OwnerId
        });
    }

    public async Task<bool> UpdatePetAsync(int id, UpdatePetRequestDto updateDto, int ownerId)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null || pet.OwnerId != ownerId)
        {
            return false;
        }

        pet.Name = updateDto.Name;
        pet.Species = updateDto.Species;
        pet.Breed = updateDto.Breed;
        pet.DateOfBirth = updateDto.DateOfBirth;

        return await _petRepository.UpdateAsync(pet);
    }
}