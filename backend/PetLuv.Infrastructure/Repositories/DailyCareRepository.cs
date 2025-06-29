using Dapper;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;
using PetLuv.Infrastructure.Data;

namespace PetLuv.Infrastructure.Repositories;

public class DailyCareRepository : IDailyCareRepository
{
    private readonly DapperContext _context;

    public DailyCareRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<DailyCare> AddAsync(DailyCare dailyCare)
    {
        var sql = @"
            INSERT INTO DailyCares (PetId, Date, FoodConsumed, WaterConsumed, Notes)
            VALUES (@PetId, @Date, @FoodConsumed, @WaterConsumed, @Notes)
            RETURNING Id INTO :Id";
        var parameters = new DynamicParameters(dailyCare);
        parameters.Add(":Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
        
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(sql, parameters);
            dailyCare.Id = parameters.Get<int>(":Id");
            return dailyCare;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = "DELETE FROM DailyCares WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }

    public async Task<DailyCare?> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM DailyCares WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<DailyCare>(sql, new { Id = id });
        }
    }

    public async Task<IEnumerable<DailyCare>> GetByPetIdAsync(int petId)
    {
        var sql = "SELECT * FROM DailyCares WHERE PetId = @PetId ORDER BY Date DESC";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryAsync<DailyCare>(sql, new { PetId = petId });
        }
    }

    public async Task<bool> UpdateAsync(DailyCare dailyCare)
    {
        var sql = @"
            UPDATE DailyCares SET
                Date = @Date,
                FoodConsumed = @FoodConsumed,
                WaterConsumed = @WaterConsumed,
                Notes = @Notes
            WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var affectedRows = await connection.ExecuteAsync(sql, dailyCare);
            return affectedRows > 0;
        }
    }
}