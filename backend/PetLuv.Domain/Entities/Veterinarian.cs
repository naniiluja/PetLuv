namespace PetLuv.Domain.Entities;

public class Veterinarian
{
    public int Id { get; set; }
    public int UserId { get; set; } // Foreign key to the User entity
    public required string ClinicName { get; set; }
    public required string Specialty { get; set; }
    public string? Bio { get; set; }
    public int YearsOfExperience { get; set; }
    public required string LicenseNumber { get; set; }
    public bool IsVerified { get; set; } = false;
}