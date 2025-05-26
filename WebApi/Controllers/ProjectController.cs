using Application.DTO;
using Application.DTO.Collaborators;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly CollaboratorService _collaboratorService;
        private readonly HolidayPlanService _holidayPlanService;
        private readonly AssociationProjectCollaboratorService _associationProjectCollaboratorService;

        public ProjectController(ProjectService projectService, CollaboratorService collaboratorService, HolidayPlanService holidayPlanService, AssociationProjectCollaboratorService associationProjectCollaboratorService)
        {
            _projectService = projectService;
            _collaboratorService = collaboratorService;
            _holidayPlanService = holidayPlanService;
            _associationProjectCollaboratorService = associationProjectCollaboratorService;
        }


        // UC11
        [HttpGet("{projectId}/collaborators")]
        public async Task<ActionResult<IEnumerable<CollaboratorDTO>>> GetAllCollaborators(Guid projectId)
        {
            var result = await _collaboratorService.FindAllByProject(projectId);
            return result.ToActionResult();
        }

        [HttpGet("{projectId}/associations")]
        public async Task<ActionResult<IEnumerable<AssociationProjectCollaboratorDTO>>> GetAllAssociations(Guid projectId)
        {
            var result = await _collaboratorService.FindAllAssociationsByProject(projectId);
            return result.ToActionResult();
        }

        // UC12
        [HttpGet("{projectId}/collaborators/byPeriod")]
        public async Task<ActionResult<IEnumerable<CollaboratorDTO>>> GetAllCollaboratorsByPeriod(Guid projectId, [FromQuery] PeriodDate periodDate)
        {
            var result = await _collaboratorService.FindAllByProjectAndBetweenPeriod(projectId, periodDate);
            return result.ToActionResult();
        }

        // UC16 : Como gestor de projeto, quero listar quantos dias de férias um colaborador tem marcado durante um projeto
        [HttpGet("{projectId}/collaborators/{collaboratorId}/holidays/count")]
        public async Task<ActionResult<int>> GetHolidayCountByCollaborator(Guid projectId, Guid collaboratorId)
        {
            var count = await _holidayPlanService.GetHolidayDaysOfCollaboratorInProjectAsync(projectId, collaboratorId);
            return count.ToActionResult();
        }

        // UC21: Como gestor de projeto, quero listar os períodos de férias dos colaboradores dum projeto, num período
        [HttpGet("{projectId}/collaborators/holidays/byPeriod")]
        public async Task<ActionResult<IEnumerable<HolidayPeriodDTO>>> GetHolidaysByProjectAndBetweenPeriod(Guid projectId, [FromQuery] PeriodDate periodDate)
        {
            var holidays = await _holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(projectId, periodDate);
            return holidays.ToActionResult();
        }

        //UC22: Como gestor de projeto, quero listar quantos dias de férias dum colaborador do projeto tem num dado período
        [HttpGet("{projectId}/collaborators/{collaboratorId}/holidays/count/byPeriod")]
        public async Task<ActionResult<int>> GetHolidayCountByCollaboratorByPeriod(Guid projectId, Guid collaboratorId, [FromQuery] PeriodDate periodDate)
        {
            var count = await _holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectId, collaboratorId, periodDate);
            return count.ToActionResult();
        }

        //UC23: Como gestor de projeto, quero listar a quantidade de dias de férias de todos os colaboradores do projeto num dado período
        [HttpGet("{projectId}/collaborators/holidays/count/byPeriod")]
        public async Task<ActionResult<int>> GetHolidayCountForAllCollaboratorsByPeriod(Guid projectId, [FromQuery] PeriodDate periodDate)
        {
            var count = await _holidayPlanService.GetHolidayDaysForProjectAllCollaboratorBetweenDates(projectId, periodDate);
            return count.ToActionResult();
        }

        // UC4: Como gestor de projetos, quero criar projeto
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> Add(CreateProjectDTO projectDTO)
        {
            var result = await _projectService.Add(projectDTO);

            return result.ToActionResult();
        }

        // UC3: Como gestor de projeto, quero associar colaborador a projeto
        [HttpPost("{projectId}/collaborators")]
        public async Task<ActionResult<AssociationProjectCollaboratorDTO>> AddCollaborator(Guid projectId, [FromBody] CreateAssociationProjectCollaboratorDTO associationDTO)
        {
            var result = await _associationProjectCollaboratorService.Add(associationDTO.PeriodDate, associationDTO.CollaboratorId, projectId);

            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAll()
        {
            var result = await _projectService.GetAll();

            return result.ToActionResult();
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDTO>> GetById(Guid projectId)
        {
            var result = await _projectService.GetProjectById(projectId);

            return result.ToActionResult();
        }
    }
}
