using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;

namespace PetLuv.Application.Services;

public class VeterinarianService : IVeterinarianService
{
    private readonly IVeterinarianRepository _veterinarianRepository;

    public VeterinarianService(IVeterinarianRepository veterinarianRepository)
    {
        _veterinarianRepository = veterinarianRepository;
    }

    public async Task<VeterinarianDto?> CreateProfileAsync(int userId, CreateVeterinarianProfileDto createDto)
    {
        var existingProfile = await _veterinarianRepository.GetByUserIdAsync(userId);
        if (existingProfile != null)
        {
            // Profile already exists for this user
            return null;
        }

        var veterinarian = new Veterinarian
        {
            UserId = userId,
            ClinicName = createDto.ClinicName,
            Specialty = createDto.Specialty,
            Bio = createDto.Bio,
            YearsOfExperience = createDto.YearsOfExperience,
            LicenseNumber = createDto.LicenseNumber,
            IsVerified = false // Verification is a separate process
        };

        var createdProfile = await _veterinarianRepository.AddAsync(veterinarian);

        return new VeterinarianDto
        {
            Id = createdProfile.Id,
            UserId = createdProfile.UserId,
            ClinicName = createdProfile.ClinicName,
            Specialty = createdProfile.Specialty,
            Bio = createdProfile.Bio,
            YearsOfExperience = createdProfile.YearsOfExperience,
            IsVerified = createdProfile.IsVerified
        };
    }

    public async Task<VeterinarianDto?> GetProfileByUserIdAsync(int userId)
    {
        var profile = await _veterinarianRepository.GetByUserIdAsync(userId);
        if (profile == null)
        {
            return null;
        }

        return new VeterinarianDto
        {
            Id = profile.Id,
            UserId = profile.UserId,
            ClinicName = profile.ClinicName,
            Specialty = profile.Specialty,
            Bio = profile.Bio,
            YearsOfExperience = profile.YearsOfExperience,
            IsVerified = profile.IsVerified
        };
    }

    public async Task<IEnumerable<VeterinarianDto>> SearchVeterinariansAsync(string? specialty, string? location)
    {
        var veterinarians = await _veterinarianRepository.SearchAsync(specialty, location);
        return veterinarians.Select(v => new VeterinarianDto
        {
            Id = v.Id,
            UserId = v.UserId,
            ClinicName = v.ClinicName,
            Specialty = v.Specialty,
            Bio = v.Bio,
            YearsOfExperience = v.YearsOfExperience,
            IsVerified = v.IsVerified
        });
    }

    public async Task<VeterinarianDto?> UpdateProfileAsync(int userId, UpdateVeterinarianProfileDto updateDto)
    {
        var profile = await _veterinarianRepository.GetByUserIdAsync(userId);
        if (profile == null)
        {
            return null;
        }

        profile.ClinicName = updateDto.ClinicName;
        profile.Specialty = updateDto.Specialty;
        profile.Bio = updateDto.Bio;
        profile.YearsOfExperience = updateDto.YearsOfExperience;

        await _veterinarianRepository.UpdateAsync(profile);

        return await GetProfileByUserIdAsync(userId);
    }
}