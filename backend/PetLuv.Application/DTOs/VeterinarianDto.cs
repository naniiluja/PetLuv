namespace PetLuv.Application.DTOs;

public class VeterinarianDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ClinicName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsVerified { get; set; }
    // We don't expose the license number in the DTO for privacy
}