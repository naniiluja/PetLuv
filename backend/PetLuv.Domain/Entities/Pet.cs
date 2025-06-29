using System;

namespace PetLuv.Domain.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
    }
}