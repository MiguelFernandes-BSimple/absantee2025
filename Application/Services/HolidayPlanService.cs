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
    private readonly IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
    private readonly IHolidayPlanRepository _holidayPlanRepository;
    private readonly IHolidayPlanFactory _holidayPlanFactory;
    private readonly IHolidayPeriodFactory _holidayPeriodFactory;
    private readonly IMapper _mapper;

    public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository, IHolidayPlanFactory holidayPlanFactory, IHolidayPeriodFactory holidayPeriodFactory, ICollaboratorRepository collaboratorRepository, IMapper mapper)
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
    public async Task<HolidayPeriodDTO> AddHolidayPeriod(Guid collabId, CreateHolidayPeriodDTO holidayPeriodDTO)
    {
        HolidayPeriod holidayPeriod;
        try
        {
            var holidayPlan = await _holidayPlanRepository.FindHolidayPlanByCollaboratorAsync(collabId);
            holidayPeriod = await _holidayPeriodFactory.Create(holidayPlan!.Id, holidayPeriodDTO.InitDate, holidayPeriodDTO.FinalDate);
            await _holidayPlanRepository.AddHolidayPeriodAsync(holidayPlan.Id, holidayPeriod);
            return _mapper.Map<HolidayPeriod, HolidayPeriodDTO>(holidayPeriod);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<HolidayPeriodDTO>> FindHolidayPeriodForCollaborator(Guid collaboratorId)
    {
        var holidayPeriods = await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorAsync(collaboratorId);

        return holidayPeriods.Select(_mapper.Map<HolidayPeriod, HolidayPeriodDTO>);
    }

    public async Task<HolidayPeriod> UpdateHolidayPeriodForCollaborator(Guid collabId, HolidayPeriodDTO periodDTO)
    {
        var period = _mapper.Map<HolidayPeriodDTO, HolidayPeriod>(periodDTO);
        return await _holidayPlanRepository.UpdateHolidayPeriodAsync(collabId, period);
    }


    //UC16: Como gestor de projeto, quero listar quantos dias de férias um colaborador tem marcado durante um projeto
    public async Task<Result<int>> GetHolidayDaysOfCollaboratorInProjectAsync(Guid projectId, Guid collaboratorId)
    {
        try
        {
            var association = await _associationProjectCollaboratorRepository.FindByProjectAndCollaboratorAsync(projectId, collaboratorId) ?? throw new Exception("A associação com os parâmetros fornecidos não existe.");

            int numberOfHolidayDays = 0;

            var collaboratorHolidayPlan = await _holidayPlanRepository.FindHolidayPlanByCollaboratorAsync(collaboratorId);

            if (collaboratorHolidayPlan == null)
                return Result<int>.Success(numberOfHolidayDays);

            numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
                association.PeriodDate
            );

            return Result<int>.Success(numberOfHolidayDays);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure(Error.InternalServerError(ex.Message));
        }
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
    private async Task<IEnumerable<HolidayPeriod>> GetIntersectingHolidayPeriodsForProjectCollaboratorsAsync(Guid projectId, PeriodDate period)
    {
        var associations = await _associationProjectCollaboratorRepository.FindAllByProjectAndIntersectingPeriodAsync(projectId, period);
        var collaboratorIds = associations.Select(a => a.CollaboratorId).Distinct().ToList();

        return await _holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsIntersectingPeriodAsync(collaboratorIds, period);
    }

    public async Task<Result<IEnumerable<HolidayPeriodDTO>>> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(Guid projectId, PeriodDate period)
    {
        try
        {
            var holidayPeriods = await GetIntersectingHolidayPeriodsForProjectCollaboratorsAsync(projectId, period);

            var result = holidayPeriods.Select(h => new HolidayPeriodDTO
            {
                PeriodDate = h.PeriodDate.GetIntersection(period)!
            });

            return Result<IEnumerable<HolidayPeriodDTO>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<HolidayPeriodDTO>>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task<Result<int>> GetHolidayDaysForProjectCollaboratorBetweenDates(Guid projectId, Guid collaboratorId, PeriodDate periodDate)
    {
        try
        {
            var associations = await _associationProjectCollaboratorRepository.FindAllByProjectAndCollaboratorAndBetweenPeriodAsync(projectId, collaboratorId, periodDate);

            int totalHolidayDays = 0;
            var holidayPeriods = await _holidayPlanRepository.FindHolidayPeriodsByCollaboratorIntersectingPeriodDate(collaboratorId, periodDate);

            foreach (var holidayColabPeriod in holidayPeriods)
            {
                totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate);
            }

            return Result<int>.Success(totalHolidayDays);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task<Result<int>> GetHolidayDaysForProjectAllCollaboratorBetweenDates(Guid projectId, PeriodDate searchPeriod)
    {
        try
        {
            var holidayPeriods = await GetIntersectingHolidayPeriodsForProjectCollaboratorsAsync(projectId, searchPeriod);

            var result = holidayPeriods.Sum(period =>
                period.GetNumberOfCommonUtilDaysBetweenPeriods(searchPeriod));

            return Result<int>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure(Error.InternalServerError(ex.Message));
        }
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
