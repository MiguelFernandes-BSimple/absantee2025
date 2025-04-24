using Application.DTO;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<IProject?>> GetProjectById(long id)
        {
            var result = await _projectService.GetProjectById(id);
            return Ok(result);
        }

        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<IProject>>> GetAllProjects()
        {
            var result = await _projectService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddCollaborator(ProjectDTO projectDTO)
        {
            bool result = await _projectService.Add(projectDTO);

            return Ok(result);
        }
    }
}
