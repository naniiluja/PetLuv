using Dapper;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;
using PetLuv.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetLuv.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            var sql = "INSERT INTO Users (Email, FullName, PasswordHash, CreatedAt) VALUES (:Email, :FullName, :PasswordHash, :CreatedAt) RETURNING Id INTO :Id";
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters(user);
                parameters.Add(":Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                await connection.ExecuteAsync(sql, parameters);
                user.Id = parameters.Get<int>(":Id");
                return user;
            }
        }

        public async Task AddUserRoleAsync(int userId, int roleId)
        {
            var sql = "INSERT INTO UserRoles (UserId, RoleId) VALUES (:UserId, :RoleId)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { UserId = userId, RoleId = roleId });
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = :Email";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User?>(sql, new { Email = email });
            }
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            var sql = "SELECT r.Name FROM Roles r INNER JOIN UserRoles ur ON r.Id = ur.RoleId WHERE ur.UserId = :UserId";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<string>(sql, new { UserId = userId });
            }
        }
    }
}