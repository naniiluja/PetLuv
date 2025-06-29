using PetLuv.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetLuv.Application.Interfaces
{
    public interface IPetRepository
    {
        Task<Pet?> GetByIdAsync(int id);
        Task<IEnumerable<Pet>> GetByOwnerIdAsync(int ownerId);
        Task<Pet> AddAsync(Pet pet);
        Task<bool> UpdateAsync(Pet pet);
        Task<bool> DeleteAsync(int id);
    }
}