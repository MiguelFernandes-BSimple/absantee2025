using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Services;
public class HolidayPlanService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;

    public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
    {
        this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        this._holidayPlanRepository = holidayPlanRepository;
    }

    //UC16: Como gestor de projeto, quero listar quantos dias de férias um colaborador tem marcado durante um projeto
    public async Task<int> GetHolidayDaysOfCollaboratorInProjectAsync(long projectId, long collaboratorId)
    {
        var association = await _associationProjectCollaboratorRepository.FindByProjectAndCollaboratorAsync(projectId, collaboratorId) ?? throw new Exception("A associação com os parâmetros fornecidos não existe.");

        int numberOfHolidayDays = 0;

        var collaboratorHolidayPlan = await _holidayPlanRepository.FindHolidayPlanByCollaboratorAsync(collaboratorId);

        if (collaboratorHolidayPlan == null)
        {
            return numberOfHolidayDays;
        }

        numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
            association._periodDate
        );

        return numberOfHolidayDays;
    }


    // UC19 - Given a collaborator and a period to search for, return the holiday periods that contain weekends.
    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(long collaboratorId, PeriodDate searchPeriod)
    {
        if (!searchPeriod.ContainsWeekend())
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> holidayPeriodsBetweenDates =
            await _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(collaboratorId, searchPeriod);

        IEnumerable<IHolidayPeriod> hp = holidayPeriodsBetweenDates
            .Where(hp => hp.ContainsWeekend());

        return hp;
    }

    // UC20 - Given 2 collaborators and a period to search for, return the overlapping holiday periods they have.
    public async Task<IEnumerable<IHolidayPeriod>> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDatesAsync(long collaboratorId1, long collaboratorId2, PeriodDate searchPeriod)
    {
        IEnumerable<IHolidayPeriod> holidayPeriodListColab1 =
            await _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(collaboratorId1, searchPeriod);

        IEnumerable<IHolidayPeriod> holidayPeriodListColab2 =
            await _holidayPlanRepository.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(collaboratorId2, searchPeriod);

        IEnumerable<IHolidayPeriod> hp = holidayPeriodListColab1
            .SelectMany(period1 => holidayPeriodListColab2
                    .Where(period2 => period1.Intersects(period2))
                    .SelectMany(period2 => new List<IHolidayPeriod> { period1, period2 }))
                    .Distinct();

        return hp;
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(long projectId, PeriodDate period)
    {
        var associations = await _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriodAsync(projectId, period);

        var collaboratorsIds = associations.Select(a => a.GetCollaboratorId());

        return await _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(collaboratorsIds.ToList(), period);

    }

    public async Task<int> GetHolidayDaysForProjectCollaboratorBetweenDates(long projectId, long collaboratorId, PeriodDate periodDate)
    {
        var association = await _associationProjectCollaboratorRepository.FindByProjectAndCollaboratorAsync(projectId, collaboratorId);
        if (association == null)
        {
            throw new Exception("No association found for the project and collaborator");
        }


        int totalHolidayDays = 0;
        var holidayPeriods = await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collaboratorId, periodDate);

        foreach (var holidayColabPeriod in holidayPeriods)
        {

            totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDays();
        }

        return totalHolidayDays;
    }

    public async Task<int> GetHolidayDaysForProjectAllCollaboratorBetweenDates(long projectId, PeriodDate searchPeriod)
    {
        var associations = await _associationProjectCollaboratorRepository.FindAllByProjectAsync(projectId);

        int totalHolidayDays = 0;

        var collabList = associations.Select(a => a.GetCollaboratorId());

        var holidayPeriods = await _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(collabList.ToList(), searchPeriod);

        foreach (var period in holidayPeriods)
        {
            totalHolidayDays += period.GetDuration();
        }

        return totalHolidayDays;
    }
}
