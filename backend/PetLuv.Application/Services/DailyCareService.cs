using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;

namespace PetLuv.Application.Services;

public class DailyCareService : IDailyCareService
{
    private readonly IDailyCareRepository _dailyCareRepository;
    private readonly IPetRepository _petRepository;

    public DailyCareService(IDailyCareRepository dailyCareRepository, IPetRepository petRepository)
    {
        _dailyCareRepository = dailyCareRepository;
        _petRepository = petRepository;
    }

    private async Task<bool> VerifyPetOwner(int petId, int ownerId)
    {
        var pet = await _petRepository.GetByIdAsync(petId);
        return pet != null && pet.OwnerId == ownerId;
    }

    public async Task<DailyCareDto> CreateDailyCareAsync(CreateDailyCareRequestDto createDto, int ownerId)
    {
        if (!await VerifyPetOwner(createDto.PetId, ownerId))
        {
            throw new UnauthorizedAccessException("User is not the owner of the pet.");
        }

        var dailyCare = new DailyCare
        {
            PetId = createDto.PetId,
            Date = createDto.Date,
            FoodConsumed = createDto.FoodConsumed,
            WaterConsumed = createDto.WaterConsumed,
            Notes = createDto.Notes
        };

        var createdCare = await _dailyCareRepository.AddAsync(dailyCare);

        return new DailyCareDto
        {
            Id = createdCare.Id,
            PetId = createdCare.PetId,
            Date = createdCare.Date,
            FoodConsumed = createdCare.FoodConsumed,
            WaterConsumed = createdCare.WaterConsumed,
            Notes = createdCare.Notes
        };
    }

    public async Task<bool> DeleteDailyCareAsync(int careId, int ownerId)
    {
        var care = await _dailyCareRepository.GetByIdAsync(careId);
        if (care == null || !await VerifyPetOwner(care.PetId, ownerId))
        {
            return false;
        }

        return await _dailyCareRepository.DeleteAsync(careId);
    }

    public async Task<DailyCareDto?> GetDailyCareAsync(int careId, int petId, int ownerId)
    {
        if (!await VerifyPetOwner(petId, ownerId))
        {
            return null;
        }

        var care = await _dailyCareRepository.GetByIdAsync(careId);
        if (care == null || care.PetId != petId)
        {
            return null;
        }

        return new DailyCareDto
        {
            Id = care.Id,
            PetId = care.PetId,
            Date = care.Date,
            FoodConsumed = care.FoodConsumed,
            WaterConsumed = care.WaterConsumed,
            Notes = care.Notes
        };
    }

    public async Task<IEnumerable<DailyCareDto>> GetDailyCaresAsync(int petId, int ownerId)
    {
        if (!await VerifyPetOwner(petId, ownerId))
        {
            return Enumerable.Empty<DailyCareDto>();
        }

        var cares = await _dailyCareRepository.GetByPetIdAsync(petId);
        return cares.Select(c => new DailyCareDto
        {
            Id = c.Id,
            PetId = c.PetId,
            Date = c.Date,
            FoodConsumed = c.FoodConsumed,
            WaterConsumed = c.WaterConsumed,
            Notes = c.Notes
        });
    }

    public async Task<bool> UpdateDailyCareAsync(int careId, UpdateDailyCareRequestDto updateDto, int ownerId)
    {
        var care = await _dailyCareRepository.GetByIdAsync(careId);
        if (care == null || !await VerifyPetOwner(care.PetId, ownerId))
        {
            return false;
        }

        care.Date = updateDto.Date;
        care.FoodConsumed = updateDto.FoodConsumed;
        care.WaterConsumed = updateDto.WaterConsumed;
        care.Notes = updateDto.Notes;

        return await _dailyCareRepository.UpdateAsync(care);
    }
}