using PetLuv.Application.DTOs;
using System.Threading.Tasks;

namespace PetLuv.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterRequestDto registerRequest);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    }
}