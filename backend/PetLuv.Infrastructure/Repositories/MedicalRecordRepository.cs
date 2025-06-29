using Dapper;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;
using PetLuv.Infrastructure.Data;

namespace PetLuv.Infrastructure.Repositories;

public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly DapperContext _context;

    public MedicalRecordRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<MedicalRecord> AddAsync(MedicalRecord medicalRecord)
    {
        var sql = @"
            INSERT INTO MedicalRecords (PetId, VisitDate, Diagnosis, Treatment, Notes, VeterinarianId)
            VALUES (@PetId, @VisitDate, @Diagnosis, @Treatment, @Notes, @VeterinarianId)
            RETURNING Id INTO :Id";
        var parameters = new DynamicParameters(medicalRecord);
        parameters.Add(":Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
        
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(sql, parameters);
            medicalRecord.Id = parameters.Get<int>(":Id");
            return medicalRecord;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = "DELETE FROM MedicalRecords WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }

    public async Task<MedicalRecord?> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM MedicalRecords WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<MedicalRecord>(sql, new { Id = id });
        }
    }

    public async Task<IEnumerable<MedicalRecord>> GetByPetIdAsync(int petId)
    {
        var sql = "SELECT * FROM MedicalRecords WHERE PetId = @PetId ORDER BY VisitDate DESC";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryAsync<MedicalRecord>(sql, new { PetId = petId });
        }
    }

    public async Task<bool> UpdateAsync(MedicalRecord medicalRecord)
    {
        var sql = @"
            UPDATE MedicalRecords SET
                VisitDate = @VisitDate,
                Diagnosis = @Diagnosis,
                Treatment = @Treatment,
                Notes = @Notes,
                VeterinarianId = @VeterinarianId
            WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var affectedRows = await connection.ExecuteAsync(sql, medicalRecord);
            return affectedRows > 0;
        }
    }
}