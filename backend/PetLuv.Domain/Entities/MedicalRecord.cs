namespace PetLuv.Domain.Entities;

public class MedicalRecord
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public DateTime VisitDate { get; set; }
    public required string Diagnosis { get; set; }
    public required string Treatment { get; set; }
    public string? Notes { get; set; }
    public int? VeterinarianId { get; set; }
}