using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly CollaboratorService _colaboratorService;

        public CollaboratorController(CollaboratorService colaboratorService)
        {
            _colaboratorService = colaboratorService;
        }

        // Post: api/Colaborator
        [HttpPost]
        public async Task<ActionResult> AddCollaborator()
        {
            long userId = 1;
            var periodDate = new PeriodDateTime(DateTime.Today, DateTime.Today.AddDays(3));
            bool result = await _colaboratorService.Add(userId, periodDate);

            return Ok();
        }
    }
}
