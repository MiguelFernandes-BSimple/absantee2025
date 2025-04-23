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
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        // Post: api/Project
        [HttpPost("addprojects")]
        public async Task<ActionResult<Project>> AddProjects([FromBody] Project project)
        {
            bool result = await _projectService.Add(project);
            return Ok(result);
        }
        // Get: api/Project
        [HttpGet("getprojects")]
        public async Task<ActionResult<Project>> GetProjects()
        {
            var result = await _projectService.GetAll();
            return Ok(result);
        }
    }
}