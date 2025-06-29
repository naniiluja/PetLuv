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
    // This controller will be implemented in a future task.
    // For now, it serves as a placeholder for the API structure.

    [HttpGet]
    public Task<ActionResult<IEnumerable<MedicalRecordDto>>> GetMedicalRecords(int petId)
    {
        // Logic to verify pet ownership and get records will be added later.
        return Task.FromResult<ActionResult<IEnumerable<MedicalRecordDto>>>(Ok(new List<MedicalRecordDto>()));
    }

    [HttpGet("{recordId}")]
    public Task<ActionResult<MedicalRecordDto>> GetMedicalRecord(int petId, int recordId)
    {
        return Task.FromResult<ActionResult<MedicalRecordDto>>(Ok(new MedicalRecordDto { Id = recordId, PetId = petId, Diagnosis = "Placeholder", Treatment = "Placeholder" }));
    }

    [HttpPost]
    public Task<ActionResult<MedicalRecordDto>> CreateMedicalRecord(int petId, CreateMedicalRecordRequestDto createDto)
    {
        var newRecord = new MedicalRecordDto
        {
            Id = new Random().Next(1, 1000),
            PetId = petId,
            VisitDate = createDto.VisitDate,
            Diagnosis = createDto.Diagnosis,
            Treatment = createDto.Treatment,
            Notes = createDto.Notes,
            VeterinarianId = createDto.VeterinarianId
        };
        return Task.FromResult<ActionResult<MedicalRecordDto>>(CreatedAtAction(nameof(GetMedicalRecord), new { petId = petId, recordId = newRecord.Id }, newRecord));
    }
}