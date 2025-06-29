using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using System.Security.Claims;

namespace PetLuv.API.Controllers;

[ApiController]
[Route("api/veterinarians")]
public class VeterinarianController : ControllerBase
{
    private readonly IVeterinarianService _veterinarianService;

    public VeterinarianController(IVeterinarianService veterinarianService)
    {
        _veterinarianService = veterinarianService;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
    }

    [HttpPost("profile")]
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> CreateProfile(CreateVeterinarianProfileDto createDto)
    {
        var userId = GetCurrentUserId();
        var result = await _veterinarianService.CreateProfileAsync(userId, createDto);
        if (result == null)
        {
            return BadRequest("A profile for this user already exists.");
        }
        return CreatedAtAction(nameof(GetMyProfile), new { }, result);
    }

    [HttpGet("profile/me")]
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = GetCurrentUserId();
        var profile = await _veterinarianService.GetProfileByUserIdAsync(userId);
        if (profile == null)
        {
            return NotFound();
        }
        return Ok(profile);
    }

    [HttpPut("profile")]
    [Authorize(Roles = "Veterinarian")]
    public async Task<IActionResult> UpdateProfile(UpdateVeterinarianProfileDto updateDto)
    {
        var userId = GetCurrentUserId();
        var result = await _veterinarianService.UpdateProfileAsync(userId, updateDto);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SearchVeterinarians([FromQuery] string? specialty, [FromQuery] string? location)
    {
        var result = await _veterinarianService.SearchVeterinariansAsync(specialty, location);
        return Ok(result);
    }

    [HttpGet("{userId}/profile")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProfileByUserId(int userId)
    {
        var profile = await _veterinarianService.GetProfileByUserIdAsync(userId);
        if (profile == null)
        {
            return NotFound();
        }
        return Ok(profile);
    }
}