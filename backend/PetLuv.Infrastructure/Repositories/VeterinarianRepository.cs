using Dapper;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;
using PetLuv.Infrastructure.Data;
using System.Text;

namespace PetLuv.Infrastructure.Repositories;

public class VeterinarianRepository : IVeterinarianRepository
{
    private readonly DapperContext _context;

    public VeterinarianRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Veterinarian> AddAsync(Veterinarian veterinarian)
    {
        var sql = @"
            INSERT INTO Veterinarians (UserId, ClinicName, Specialty, Bio, YearsOfExperience, LicenseNumber, IsVerified)
            VALUES (@UserId, @ClinicName, @Specialty, @Bio, @YearsOfExperience, @LicenseNumber, @IsVerified)
            RETURNING Id INTO :Id";
        var parameters = new DynamicParameters(veterinarian);
        parameters.Add(":Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
        
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(sql, parameters);
            veterinarian.Id = parameters.Get<int>(":Id");
            return veterinarian;
        }
    }

    public async Task<Veterinarian?> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM Veterinarians WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Veterinarian>(sql, new { Id = id });
        }
    }

    public async Task<Veterinarian?> GetByUserIdAsync(int userId)
    {
        var sql = "SELECT * FROM Veterinarians WHERE UserId = @UserId";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Veterinarian>(sql, new { UserId = userId });
        }
    }

    public async Task<IEnumerable<Veterinarian>> SearchAsync(string? specialty, string? location)
    {
        var sql = new StringBuilder("SELECT V.*, U.Address FROM Veterinarians V JOIN Users U ON V.UserId = U.Id WHERE V.IsVerified = 1");
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(specialty))
        {
            sql.Append(" AND V.Specialty LIKE @Specialty");
            parameters.Add("Specialty", $"%{specialty}%");
        }

        if (!string.IsNullOrEmpty(location))
        {
            sql.Append(" AND U.Address LIKE @Location");
            parameters.Add("Location", $"%{location}%");
        }

        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryAsync<Veterinarian>(sql.ToString(), parameters);
        }
    }

    public async Task<bool> UpdateAsync(Veterinarian veterinarian)
    {
        var sql = @"
            UPDATE Veterinarians SET
                ClinicName = @ClinicName,
                Specialty = @Specialty,
                Bio = @Bio,
                YearsOfExperience = @YearsOfExperience,
                IsVerified = @IsVerified
            WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var affectedRows = await connection.ExecuteAsync(sql, veterinarian);
            return affectedRows > 0;
        }
    }
}