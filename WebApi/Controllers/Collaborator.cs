using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers;
public class Collaborator
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly CollaboratorService _collaboratorService;
        List<string> _errorMessages = new List<string>();
        public CollaboratorController(CollaboratorService colaboratorService)
        {
            _collaboratorService = colaboratorService;
        }
        // GET: api/Colaborator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ICollaborator>>> GetColaborators()
        {
            IEnumerable<ICollaborator> Collaborators = await _collaboratorService.GetAllCollaborator();

            return Ok(Collaborators);
        }

        // GET: api/Colaborator/5
        /* [HttpGet("{id}")]
        public async Task<ActionResult<Collaborator>> GetColaboratorById(long id)
        {
            var Collaborator = await _collaboratorService.;

            if (Collaborator == null)
            {
                return NotFound();
            }

            return Ok(Collaborator);
        } */
    }

}
