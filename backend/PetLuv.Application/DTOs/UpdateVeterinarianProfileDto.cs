using System.ComponentModel.DataAnnotations;

namespace PetLuv.Application.DTOs;

public class UpdateVeterinarianProfileDto
{
    [Required]
    [MaxLength(200)]
    public required string ClinicName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Specialty { get; set; }

    [MaxLength(2000)]
    public string? Bio { get; set; }

    [Required]
    [Range(0, 60)]
    public int YearsOfExperience { get; set; }
}