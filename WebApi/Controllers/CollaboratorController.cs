using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly CollaboratorService _collabService;

        public CollaboratorController(CollaboratorService collabService)
        {
            _collabService = collabService;
        }

        [HttpGet("FindBy")]
        public async Task<IActionResult> FindBy([FromQuery] string? name, [FromQuery] string? surname)
        {
            if (name == null && surname == null)
                return BadRequest("Please insert at least a name or surname");

            IEnumerable<Guid> collabIds;

            if (surname == null)
            {
                collabIds = await _collabService.GetByNames(name);
            }
            else if (name == null)
            {
                collabIds = await _collabService.GetBySurnames(surname);
            }
            else
            {
                collabIds = await _collabService.GetByNamesAndSurnames(name, surname);
            }

            if (collabIds.Any())
                return Ok(collabIds);

            else return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCollaboratorDto collabDto)
        {
            if (collabDto == null)
                return BadRequest("Invalid Arguments");

            var collabCreated = await _collabService.Create(collabDto);

            if (collabCreated == null) return BadRequest();

            return Created("", collabCreated);
        }

        // endpoint utilizado para testes
        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _collabService.GetCount();

            if (count > 0)
                return Ok(count);

            return NotFound("No collaborators found");
        }
    }
}