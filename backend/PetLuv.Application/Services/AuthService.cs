using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using PetLuv.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetLuv.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest)
        {
            var user = await _userRepository.GetByEmailAsync(loginRequest.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                return null;
            }

            var roles = await _userRepository.GetUserRolesAsync(user.Id);
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, roles);

            return new LoginResponseDto { Token = token };
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto registerRequest)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerRequest.Email);
            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                Email = registerRequest.Email,
                FullName = registerRequest.FullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
                CreatedAt = DateTime.UtcNow
            };

            var newUser = await _userRepository.AddAsync(user);

            // For now, assign a default role, e.g., "PetOwner" (assuming role ID 2)
            // A more robust implementation would look up the role ID by name.
            await _userRepository.AddUserRoleAsync(newUser.Id, 2); 

            return true;
        }
    }
}