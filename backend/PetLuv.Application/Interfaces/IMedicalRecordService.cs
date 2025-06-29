using PetLuv.Application.DTOs;

namespace PetLuv.Application.Interfaces;

public interface IMedicalRecordService
{
    Task<IEnumerable<MedicalRecordDto>> GetMedicalRecordsAsync(int petId, int ownerId);
    Task<MedicalRecordDto?> GetMedicalRecordAsync(int recordId, int petId, int ownerId);
    Task<MedicalRecordDto> CreateMedicalRecordAsync(CreateMedicalRecordRequestDto createDto, int ownerId);
    Task<bool> UpdateMedicalRecordAsync(int recordId, UpdateMedicalRecordRequestDto updateDto, int ownerId);
    Task<bool> DeleteMedicalRecordAsync(int recordId, int ownerId);
}