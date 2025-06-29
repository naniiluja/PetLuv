using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetLuv.Application.DTOs;
using PetLuv.Application.Interfaces;
using System.Security.Claims;

namespace PetLuv.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        private int GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User ID not found in token.");
            }
            return int.Parse(userId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetUserPets()
        {
            var ownerId = GetCurrentUserId();
            var pets = await _petService.GetPetsByOwnerAsync(ownerId);
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(int id)
        {
            var ownerId = GetCurrentUserId();
            var pet = await _petService.GetPetAsync(id, ownerId);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<PetDto>> CreatePet(CreatePetRequestDto createPetRequest)
        {
            var ownerId = GetCurrentUserId();
            var createdPet = await _petService.CreatePetAsync(createPetRequest, ownerId);
            return CreatedAtAction(nameof(GetPet), new { id = createdPet.Id }, createdPet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(int id, UpdatePetRequestDto updatePetRequest)
        {
            var ownerId = GetCurrentUserId();
            var result = await _petService.UpdatePetAsync(id, updatePetRequest, ownerId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var ownerId = GetCurrentUserId();
            var result = await _petService.DeletePetAsync(id, ownerId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}