using Dapper;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;
using PetLuv.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetLuv.Infrastructure.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly DapperContext _context;

        public PetRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Pet> AddAsync(Pet pet)
        {
            var sql = "INSERT INTO Pets (OwnerId, Name, Species, Breed, DateOfBirth, PhotoUrl) VALUES (:OwnerId, :Name, :Species, :Breed, :DateOfBirth, :PhotoUrl) RETURNING Id INTO :Id";
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters(pet);
                parameters.Add(":Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                await connection.ExecuteAsync(sql, parameters);
                pet.Id = parameters.Get<int>(":Id");
                return pet;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Pets WHERE Id = :Id";
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0;
            }
        }

        public async Task<Pet?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Pets WHERE Id = :Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Pet>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Pet>> GetByOwnerIdAsync(int ownerId)
        {
            var sql = "SELECT * FROM Pets WHERE OwnerId = :OwnerId";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Pet>(sql, new { OwnerId = ownerId });
            }
        }

        public async Task<bool> UpdateAsync(Pet pet)
        {
            var sql = "UPDATE Pets SET Name = :Name, Species = :Species, Breed = :Breed, DateOfBirth = :DateOfBirth, PhotoUrl = :PhotoUrl WHERE Id = :Id";
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, pet);
                return affectedRows > 0;
            }
        }
    }
}