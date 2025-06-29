using PetLuv.Domain.Entities;

namespace PetLuv.Application.Interfaces;

public interface IMedicalRecordRepository
{
    Task<MedicalRecord?> GetByIdAsync(int id);
    Task<IEnumerable<MedicalRecord>> GetByPetIdAsync(int petId);
    Task<MedicalRecord> AddAsync(MedicalRecord medicalRecord);
    Task<bool> UpdateAsync(MedicalRecord medicalRecord);
    Task<bool> DeleteAsync(int id);
}