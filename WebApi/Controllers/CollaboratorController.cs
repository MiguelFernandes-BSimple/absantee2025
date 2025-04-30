using Application.DTO;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/collaborators")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly CollaboratorService _collabService;
        private readonly HolidayPlanService _holidayPlanService;

        public CollaboratorController(CollaboratorService collabService, HolidayPlanService holidayPlanService)
        {
            _collabService = collabService;
            _holidayPlanService = holidayPlanService;
        }

        // Get: api/collaborators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guid>>> GetAllCollaborators() {
            var result = await _collabService.GetAll();
            return Ok(result);
        }

        // Get: api/collaborators/foo/holidayperiods?includesDate=bar
        [HttpGet("{id}/holidayperiods")]
        public async Task<ActionResult<HolidayPeriod?>> GetHolidayPeriodContainingDay(Guid id, string includesDate) {
            var dateOnly = DateOnly.Parse(includesDate);
            var result = await _holidayPlanService.FindHolidayPeriodForCollaboratorThatContainsDay(id, dateOnly);

            if(result != null)
                return Ok(result);
            
            return NotFound();
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
        //UC13
        [HttpGet("collaborators/{collaboratorId}/holidayPlan/holidayPeriods/ByPeriod")]
        public async Task<ActionResult<IEnumerable<HolidayPeriodDTO>>> GetHolidayPeriodsOfCollaboratorByPeriod(Guid collaboratorId, [FromQuery] PeriodDate periodDate)
        {
            var result = await _collabService.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collaboratorId, periodDate);

            return Ok(result);
        }



        // Post: api/Colaborator
        //[HttpPost]
        //public async Task<ActionResult> AddCollaborator()
        //{
        //    bool result = await _colaboratorService.Add(userId, periodDate);

        //        //    return Ok();
        //}
    }
}