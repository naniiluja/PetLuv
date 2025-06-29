using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using System.Security.Claims;

namespace PetLuv.API.Controllers;

[ApiController]
[Route("api/pets/{petId}/daily-cares")]
[Authorize]
public class DailyCareController : ControllerBase
{
    private readonly IDailyCareService _dailyCareService;

    public DailyCareController(IDailyCareService dailyCareService)
    {
        _dailyCareService = dailyCareService;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
    }

    [HttpPost]
    public async Task<IActionResult> CreateDailyCare(int petId, CreateDailyCareRequestDto createDto)
    {
        if (petId != createDto.PetId)
        {
            return BadRequest("PetId in URL does not match PetId in request body.");
        }

        var ownerId = GetCurrentUserId();
        try
        {
            var createdCare = await _dailyCareService.CreateDailyCareAsync(createDto, ownerId);
            return CreatedAtAction(nameof(GetDailyCare), new { petId = createdCare.PetId, careId = createdCare.Id }, createdCare);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetDailyCares(int petId)
    {
        var ownerId = GetCurrentUserId();
        var cares = await _dailyCareService.GetDailyCaresAsync(petId, ownerId);
        return Ok(cares);
    }

    [HttpGet("{careId}")]
    public async Task<IActionResult> GetDailyCare(int petId, int careId)
    {
        var ownerId = GetCurrentUserId();
        var care = await _dailyCareService.GetDailyCareAsync(careId, petId, ownerId);
        if (care == null)
        {
            return NotFound();
        }
        return Ok(care);
    }

    [HttpPut("{careId}")]
    public async Task<IActionResult> UpdateDailyCare(int petId, int careId, UpdateDailyCareRequestDto updateDto)
    {
        var ownerId = GetCurrentUserId();
        var success = await _dailyCareService.UpdateDailyCareAsync(careId, updateDto, ownerId);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{careId}")]
    public async Task<IActionResult> DeleteDailyCare(int petId, int careId)
    {
        var ownerId = GetCurrentUserId();
        var success = await _dailyCareService.DeleteDailyCareAsync(careId, ownerId);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}