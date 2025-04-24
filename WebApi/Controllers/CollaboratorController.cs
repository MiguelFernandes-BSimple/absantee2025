using Application.DTO;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
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
        public async Task<ActionResult> AddCollaborator([FromBody] CollaboratorDTO collaboratorDTO)
        {
            var result = await _colaboratorService.Add(collaboratorDTO);

            return Ok(result);
        }
    }
}
