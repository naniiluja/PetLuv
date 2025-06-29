using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;

namespace PetLuv.Application.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IPetRepository _petRepository;

    public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, IPetRepository petRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _petRepository = petRepository;
    }

    private async Task<bool> VerifyPetOwner(int petId, int ownerId)
    {
        var pet = await _petRepository.GetByIdAsync(petId);
        return pet != null && pet.OwnerId == ownerId;
    }

    public async Task<MedicalRecordDto> CreateMedicalRecordAsync(CreateMedicalRecordRequestDto createDto, int ownerId)
    {
        if (!await VerifyPetOwner(createDto.PetId, ownerId))
        {
            throw new UnauthorizedAccessException("User is not the owner of the pet.");
        }

        var medicalRecord = new MedicalRecord
        {
            PetId = createDto.PetId,
            VisitDate = createDto.VisitDate,
            Diagnosis = createDto.Diagnosis,
            Treatment = createDto.Treatment,
            Notes = createDto.Notes,
            VeterinarianId = createDto.VeterinarianId
        };

        var createdRecord = await _medicalRecordRepository.AddAsync(medicalRecord);

        return new MedicalRecordDto
        {
            Id = createdRecord.Id,
            PetId = createdRecord.PetId,
            VisitDate = createdRecord.VisitDate,
            Diagnosis = createdRecord.Diagnosis,
            Treatment = createdRecord.Treatment,
            Notes = createdRecord.Notes,
            VeterinarianId = createdRecord.VeterinarianId
        };
    }

    public async Task<bool> DeleteMedicalRecordAsync(int recordId, int ownerId)
    {
        var record = await _medicalRecordRepository.GetByIdAsync(recordId);
        if (record == null || !await VerifyPetOwner(record.PetId, ownerId))
        {
            return false;
        }

        return await _medicalRecordRepository.DeleteAsync(recordId);
    }

    public async Task<MedicalRecordDto?> GetMedicalRecordAsync(int recordId, int petId, int ownerId)
    {
        if (!await VerifyPetOwner(petId, ownerId))
        {
            return null;
        }

        var record = await _medicalRecordRepository.GetByIdAsync(recordId);
        if (record == null || record.PetId != petId)
        {
            return null;
        }

        return new MedicalRecordDto
        {
            Id = record.Id,
            PetId = record.PetId,
            VisitDate = record.VisitDate,
            Diagnosis = record.Diagnosis,
            Treatment = record.Treatment,
            Notes = record.Notes,
            VeterinarianId = record.VeterinarianId
        };
    }

    public async Task<IEnumerable<MedicalRecordDto>> GetMedicalRecordsAsync(int petId, int ownerId)
    {
        if (!await VerifyPetOwner(petId, ownerId))
        {
            return Enumerable.Empty<MedicalRecordDto>();
        }

        var records = await _medicalRecordRepository.GetByPetIdAsync(petId);
        return records.Select(r => new MedicalRecordDto
        {
            Id = r.Id,
            PetId = r.PetId,
            VisitDate = r.VisitDate,
            Diagnosis = r.Diagnosis,
            Treatment = r.Treatment,
            Notes = r.Notes,
            VeterinarianId = r.VeterinarianId
        });
    }

    public async Task<bool> UpdateMedicalRecordAsync(int recordId, UpdateMedicalRecordRequestDto updateDto, int ownerId)
    {
        var record = await _medicalRecordRepository.GetByIdAsync(recordId);
        if (record == null || !await VerifyPetOwner(record.PetId, ownerId))
        {
            return false;
        }

        record.VisitDate = updateDto.VisitDate;
        record.Diagnosis = updateDto.Diagnosis;
        record.Treatment = updateDto.Treatment;
        record.Notes = updateDto.Notes;
        record.VeterinarianId = updateDto.VeterinarianId;

        return await _medicalRecordRepository.UpdateAsync(record);
    }
}