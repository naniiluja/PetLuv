using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using System.Security.Claims;

namespace PetLuv.API.Controllers;

[ApiController]
[Route("api/pets/{petId}/medical-records")]
[Authorize]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordController(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    private int GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("User ID not found in token.");
        }
        return int.Parse(userId);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetMedicalRecords(int petId)
    {
        var ownerId = GetCurrentUserId();
        var records = await _medicalRecordService.GetMedicalRecordsAsync(petId, ownerId);
        return Ok(records);
    }

    [HttpGet("{recordId}")]
    public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecord(int petId, int recordId)
    {
        var ownerId = GetCurrentUserId();
        var record = await _medicalRecordService.GetMedicalRecordAsync(recordId, petId, ownerId);
        if (record == null)
        {
            return NotFound();
        }
        return Ok(record);
    }

    [HttpPost]
    public async Task<ActionResult<MedicalRecordDto>> CreateMedicalRecord(int petId, CreateMedicalRecordRequestDto createDto)
    {
        if (petId != createDto.PetId)
        {
            return BadRequest("Pet ID in URL does not match Pet ID in body.");
        }
        var ownerId = GetCurrentUserId();
        try
        {
            var createdRecord = await _medicalRecordService.CreateMedicalRecordAsync(createDto, ownerId);
            return CreatedAtAction(nameof(GetMedicalRecord), new { petId = createdRecord.PetId, recordId = createdRecord.Id }, createdRecord);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    [HttpPut("{recordId}")]
    public async Task<IActionResult> UpdateMedicalRecord(int petId, int recordId, UpdateMedicalRecordRequestDto updateDto)
    {
        var ownerId = GetCurrentUserId();
        var result = await _medicalRecordService.UpdateMedicalRecordAsync(recordId, updateDto, ownerId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{recordId}")]
    public async Task<IActionResult> DeleteMedicalRecord(int petId, int recordId)
    {
        var ownerId = GetCurrentUserId();
        var result = await _medicalRecordService.DeleteMedicalRecordAsync(recordId, ownerId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}