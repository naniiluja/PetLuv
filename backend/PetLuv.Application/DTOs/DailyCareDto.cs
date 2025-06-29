namespace PetLuv.Application.DTOs;

public class DailyCareDto
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public DateTime Date { get; set; }
    public string FoodConsumed { get; set; } = string.Empty;
    public string WaterConsumed { get; set; } = string.Empty;
    public string? Notes { get; set; }
}