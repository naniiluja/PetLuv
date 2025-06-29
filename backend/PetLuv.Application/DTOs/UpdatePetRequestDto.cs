using System;
using System.ComponentModel.DataAnnotations;

namespace PetLuv.Application.DTOs
{
    public class UpdatePetRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string PhotoUrl { get; set; } = string.Empty;
    }
}