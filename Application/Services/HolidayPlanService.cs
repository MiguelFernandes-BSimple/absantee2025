using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using Application.DTO;
using AutoMapper;
using Domain.Factory;
using Infrastructure.Repositories;

namespace Application.Services;
public class HolidayPlanService
{
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private IHolidayPlanRepository _holidayPlanRepository;
    private IHolidayPlanFactory _holidayPlanFactory;
    private IHolidayPeriodFactory _holidayPeriodFactory;
    private readonly IMapper _mapper;

    public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, IHolidayPlanFactory holidayPlanFactory, IHolidayPeriodFactory holidayPeriodFactory, IMapper mapper)
    {
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        _holidayPlanRepository = holidayPlanRepository;
        _holidayPlanFactory = holidayPlanFactory;
        _holidayPeriodFactory = holidayPeriodFactory;
        _mapper = mapper;
    }

    // NENHUMA UC ????
    public async Task<HolidayPlanDTO> AddHolidayPlan(CreateHolidayPlanDTO holidayPlanDTO)
    {
        HolidayPlan holidayPlan;
        try
        {
            var periodDates = holidayPlanDTO.HolidayPeriods.Select(hp => new PeriodDate(hp.InitDate, hp.FinalDate)).ToList();
            holidayPlan = await _holidayPlanFactory.Create(holidayPlanDTO.CollaboratorId, periodDates);
            var result = await _holidayPlanRepository.AddAsync(holidayPlan);
            return _mapper.Map<HolidayPlan, HolidayPlanDTO>(result);
        }
        catch (Exception)
        {
            return null;
        }
    }

    // UC1
    public async Task<HolidayPeriodDTO> AddHolidayPeriod(Guid holidayPlanId, CreateHolidayPeriodDTO holidayPeriodDTO)
    {
        HolidayPeriod holidayPeriod;
        try
        {
            holidayPeriod = await _holidayPeriodFactory.Create(holidayPlanId, holidayPeriodDTO.InitDate, holidayPeriodDTO.FinalDate);
            await _holidayPlanRepository.AddHolidayPeriodAsync(holidayPeriod);
            return _mapper.Map<HolidayPeriod, HolidayPeriodDTO>(holidayPeriod);
        }
        catch (Exception)
        {
            return null;
        }
    }


    //UC16: Como gestor de projeto, quero listar quantos dias de férias um colaborador tem marcado durante um projeto
    public async Task<int> GetHolidayDaysOfCollaboratorInProjectAsync(Guid projectId, Guid collaboratorId)
    {
        var association = await _associationProjectCollaboratorRepository.FindByProjectAndCollaboratorAsync(projectId, collaboratorId) ?? throw new Exception("A associação com os parâmetros fornecidos não existe.");

        int numberOfHolidayDays = 0;

        var collaboratorHolidayPlan = await _holidayPlanRepository.FindHolidayPlanByCollaboratorAsync(collaboratorId);

        if (collaboratorHolidayPlan == null)
        {
            return numberOfHolidayDays;
        }

        numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
            association.PeriodDate
        );

        return numberOfHolidayDays;
    }


    // UC19 - Given a collaborator and a period to search for, return the holiday periods that contain weekends.
    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(Guid collaboratorId, PeriodDate searchPeriod)
    {
        if (!searchPeriod.ContainsWeekend())
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> holidayPeriodsBetweenDates =
            await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collaboratorId, searchPeriod);

        IEnumerable<IHolidayPeriod> hp = holidayPeriodsBetweenDates
            .Where(hp => hp.ContainsWeekend());

        return hp;
    }

    // UC20 - Given 2 collaborators and a period to search for, return the overlapping holiday periods they have.
    public async Task<IEnumerable<IHolidayPeriod>> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDatesAsync(Guid collaboratorId1, Guid collaboratorId2, PeriodDate searchPeriod)
    {
        IEnumerable<IHolidayPeriod> holidayPeriodListColab1 =
            await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collaboratorId1, searchPeriod);

        IEnumerable<IHolidayPeriod> holidayPeriodListColab2 =
            await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collaboratorId2, searchPeriod);

        IEnumerable<IHolidayPeriod> hp = holidayPeriodListColab1
            .SelectMany(period1 => holidayPeriodListColab2
                    .Where(period2 => period1.Intersects(period2))
                    .SelectMany(period2 => new List<IHolidayPeriod> { period1, period2 }))
                    .Distinct();
 
        return hp; 
    }

    public async Task<IEnumerable<HolidayPeriodDTO>> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(Guid projectId, PeriodDate period)
    {
        var associations = await _associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriodAsync(projectId, period);

        var collaboratorsIds = associations.Select(a => a.CollaboratorId);

        var holidays = await _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsIntersectionPeriodAsync(collaboratorsIds.ToList(), period);
        
        var filteredPeriodsDTO = holidays.Select(h =>
        {
            var intersection = h.PeriodDate.GetIntersection(period);
            return new HolidayPeriodDTO
            {
                PeriodDate = intersection!
            };
        });
        return filteredPeriodsDTO;
    }

    public async Task<IEnumerable<HolidayPeriodDTO>> FindAllHolidayPeriodsForProjectCollaboratorBetweenDatesAsync(Guid collaboratorId, Guid projectId, PeriodDate period)
    {
        var associations = await _associationProjectCollaboratorRepository.FindAllByProjectAndCollaboratorAndBetweenPeriodAsync(projectId, collaboratorId, period);

        var holidays = await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorIntersectingPeriodDate(collaboratorId, period);

        var filteredPeriodsDTO = holidays.Select(h =>
        {
            var intersection = h.PeriodDate.GetIntersection(period);
            return new HolidayPeriodDTO
            {
                PeriodDate = intersection!
            };
        });
        return filteredPeriodsDTO;
    }

    public async Task<int> GetHolidayDaysForProjectCollaboratorBetweenDates(Guid projectId, Guid collaboratorId, PeriodDate periodDate)
    {
        var associations = await _associationProjectCollaboratorRepository.FindAllByProjectAndCollaboratorAndBetweenPeriodAsync(projectId, collaboratorId, periodDate);

        int totalHolidayDays = 0;
        var holidayPeriods = await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorIntersectingPeriodDate(collaboratorId, periodDate);

        foreach (var holidayColabPeriod in holidayPeriods)
        {

            totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate);
        }

        return totalHolidayDays;
    }

    public async Task<int> GetHolidayDaysForProjectAllCollaboratorBetweenDates(Guid projectId, PeriodDate searchPeriod)
    {
        var associations = await _associationProjectCollaboratorRepository.FindAllByProjectAsync(projectId);

        int totalHolidayDays = 0;

        var collabList = associations.Select(a => a.CollaboratorId);

        var holidayPeriods = await _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsIntersectionPeriodAsync(collabList.ToList(), searchPeriod);

        foreach (var period in holidayPeriods)
        {
            totalHolidayDays += period.GetNumberOfCommonUtilDaysBetweenPeriods(searchPeriod);
        }

        return totalHolidayDays;
    }

    public async Task<HolidayPeriod?> FindHolidayPeriodForCollaboratorThatContainsDay(Guid collabId, DateOnly dateOnly)
    {
        var holidayPeriods = await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorAsync(collabId);

        return holidayPeriods.Where(h => h.ContainsDate(dateOnly)).FirstOrDefault();
    }

    public async Task<IEnumerable<HolidayPeriod>> FindAllHolidayPeriodsForCollaboratorLongerThan(Guid collabId, int amount)
    {
        var holidayPeriods = await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorAsync(collabId);

        return holidayPeriods.Where(h => h.GetDuration() > amount);
    }
}
