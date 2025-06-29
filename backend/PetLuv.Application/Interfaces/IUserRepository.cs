using PetLuv.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetLuv.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task AddUserRoleAsync(int userId, int roleId);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    }
}