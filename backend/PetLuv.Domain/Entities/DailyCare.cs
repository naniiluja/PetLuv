namespace PetLuv.Domain.Entities;

public class DailyCare
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public DateTime Date { get; set; }
    public required string FoodConsumed { get; set; }
    public required string WaterConsumed { get; set; }
    public string? Notes { get; set; }
}