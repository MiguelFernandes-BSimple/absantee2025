using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly CollaboratorService _colaboratorService;

        public ProjectController(CollaboratorService colaboratorService)
        {
            _colaboratorService = colaboratorService;
        }

        // Post: api/Colaborator
        [HttpPost]
        public async Task<ActionResult> AddProjects([FromBody] CollaboratorDataModel collaboratorDataModel)
        {
            // long userId = 1;
            // var periodDate = new PeriodDateTime(DateTime.Today, DateTime.Today.AddDays(3));
            bool result = await _colaboratorService.Add(collaboratorDataModel.UserId, collaboratorDataModel.PeriodDateTime);

            return Ok(result);
        }
    }
}