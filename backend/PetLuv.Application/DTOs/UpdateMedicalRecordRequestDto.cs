using System.ComponentModel.DataAnnotations;

namespace PetLuv.Application.DTOs;

public class UpdateMedicalRecordRequestDto
{
    [Required]
    public DateTime VisitDate { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Diagnosis { get; set; }

    [Required]
    [MaxLength(1000)]
    public required string Treatment { get; set; }

    [MaxLength(2000)]
    public string? Notes { get; set; }

    public int? VeterinarianId { get; set; }
}