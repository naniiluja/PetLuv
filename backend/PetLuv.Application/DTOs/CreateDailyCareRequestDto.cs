using System.ComponentModel.DataAnnotations;

namespace PetLuv.Application.DTOs;

public class CreateDailyCareRequestDto
{
    [Required]
    public int PetId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(200)]
    public required string FoodConsumed { get; set; }

    [Required]
    [MaxLength(200)]
    public required string WaterConsumed { get; set; }

    [MaxLength(2000)]
    public string? Notes { get; set; }
}