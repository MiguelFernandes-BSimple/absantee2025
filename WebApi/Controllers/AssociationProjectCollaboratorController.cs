using Application.DTO;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociationProjectCollaboratorController : ControllerBase
    {
        private readonly AssociationProjectCollaboratorService _apcService;

        public AssociationProjectCollaboratorController(AssociationProjectCollaboratorService apcService)
        {
            _apcService = apcService;
        }

        // POST : api/AssociationProjectCollaborator
        [HttpPost]
        public async Task<ActionResult> AddAssociationProjectCollaborator(AssociationProjectCollaboratorDTO dto) {
            bool result = await _apcService.Add(dto.PeriodDate, dto.CollabId, dto.ProjectId);

            if(result)
                return Ok();
            
            return BadRequest();
        }
    }
}
