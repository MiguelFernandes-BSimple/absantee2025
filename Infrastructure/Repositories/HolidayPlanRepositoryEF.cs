using Domain.Models;
using Infrastructure.DataModel;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class HolidayPlanRepositoryEF : GenericRepositoryEF<IHolidayPlan, HolidayPlan, HolidayPlanDataModel>, IHolidayPlanRepository
{
    private IMapper _mapper;

    public HolidayPlanRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<bool> CanInsertHolidayPlan(Guid collaboratorId)
    {
        return !await _context.Set<HolidayPlanDataModel>().AnyAsync(hp => hp.CollaboratorId == collaboratorId);
    }

    public async Task<bool> CanInsertHolidayPeriod(Guid holidayPlanId, HolidayPeriod periodDate)
    {
        return !await _context.Set<HolidayPlanDataModel>().AnyAsync
            (h => h.Id == holidayPlanId && h.HolidayPeriods.Any
                (hp => hp.PeriodDate.InitDate <= periodDate.PeriodDate.InitDate
                    && hp.PeriodDate.FinalDate >= periodDate.PeriodDate.FinalDate));
    }

    public async Task<HolidayPeriod> AddHolidayPeriodAsync(Guid holidayPlanId, HolidayPeriod holidayPeriod)
    {
        var holidayPlan = await _context.Set<HolidayPlanDataModel>()
            .Include(h => h.HolidayPeriods)
            .FirstOrDefaultAsync(h => h.Id == holidayPlanId);

        if (holidayPlan == null)
            throw new Exception("Holiday Plan doesn't exist");

        var dataModel = _mapper.Map<HolidayPeriod, HolidayPeriodDataModel>(holidayPeriod);

        holidayPlan.HolidayPeriods.Add(dataModel);

        await SaveChangesAsync();

        return _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(dataModel);
    }

    public async Task<HolidayPeriod> UpdateHolidayPeriodAsync(Guid collabId, HolidayPeriod holidayPeriod)
    {
        var holidayPlan = await _context.Set<HolidayPlanDataModel>()
            .Include(h => h.HolidayPeriods)
            .FirstOrDefaultAsync(h => h.CollaboratorId == collabId);

        if (holidayPlan == null)
            throw new KeyNotFoundException("HolidayPlan not found");

        var period = holidayPlan.HolidayPeriods
            .FirstOrDefault(p => p.Id == holidayPeriod.Id);

        if (period == null)
            throw new KeyNotFoundException("HolidayPeriod not found");

        _mapper.Map(holidayPeriod, period);
        await SaveChangesAsync();

        return _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(period);
    }

    public async Task<IEnumerable<HolidayPlan>> FindAllCollaboratorsWithHolidayPeriodsBetweenDatesAsync(PeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriods.Any(h => periodDate.Contains(h.PeriodDate)))
            .Select(hp => _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hp))
            .ToListAsync();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<Guid> collabIds, PeriodDate periodDate)
    {
        var ret = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => collabIds.Contains(hp.CollaboratorId))
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(hperiod => periodDate.InitDate <= hperiod.PeriodDate.InitDate
                     && periodDate.FinalDate >= hperiod.PeriodDate.FinalDate)
            .ToListAsync();

        return ret.Select(_mapper.Map<HolidayPeriodDataModel, IHolidayPeriod>);
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsIntersectingPeriodAsync(List<Guid> collabIds, PeriodDate periodDate)
    {
        var ret = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => collabIds.Contains(hp.CollaboratorId))
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(hperiod => periodDate.InitDate <= hperiod.PeriodDate.FinalDate
                    && periodDate.FinalDate >= hperiod.PeriodDate.InitDate)
            .ToListAsync();

        return ret.Select(_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>);
    }

    public async Task<IEnumerable<HolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate)
    {

        var holidayPeriodsDM = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .Include(hp => hp.HolidayPeriods)
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(hperiod => periodDate.InitDate <= hperiod.PeriodDate.InitDate
                    && periodDate.FinalDate >= hperiod.PeriodDate.FinalDate)
            .ToListAsync();

        return holidayPeriodsDM.Select(_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>);
    }

    public async Task<IEnumerable<HolidayPeriod>> FindHolidayPeriodsByCollaboratorIntersectingPeriodDate(Guid collaboratorId, PeriodDate periodDate)
    {

        var holidayPeriodsDM = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .Include(hp => hp.HolidayPeriods)
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(hperiod => periodDate.InitDate <= hperiod.PeriodDate.FinalDate
                    && periodDate.FinalDate >= hperiod.PeriodDate.InitDate)
            .ToListAsync();

        return holidayPeriodsDM.Select(_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>);
    }

    public async Task<IEnumerable<HolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate, int days)
    {
        try
        {
            HolidayPlanDataModel? holidayPlan =
                await _context.Set<HolidayPlanDataModel>()
                              .FirstOrDefaultAsync(hp => hp.CollaboratorId == collaboratorId);

            if (holidayPlan == null)
                return new List<HolidayPeriod>();

            List<HolidayPeriodDataModel> ret =
                holidayPlan.HolidayPeriods.Where(h => periodDate.Contains(h.PeriodDate) &&
                                                (periodDate.Duration() > days))
                                          .ToList();

            return ret.Select(_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriods
                .Any(h => (h.PeriodDate.FinalDate.DayNumber - h.PeriodDate.InitDate.DayNumber + 1) > days))
            .Include(hp => hp.HolidayPeriods)
            .ToListAsync();

        return hpDm.Select(_mapper.Map<HolidayPlanDataModel, HolidayPlan>);
    }

    public async Task<IEnumerable<HolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(Guid collaboratorId)
    {
        var holidayPlans = await _context.Set<HolidayPlanDataModel>()
            .Include(hp => hp.HolidayPeriods)
            .FirstOrDefaultAsync(hp => hp.CollaboratorId == collaboratorId);

        if (holidayPlans == null)
            return Enumerable.Empty<HolidayPeriod>();

        return holidayPlans.HolidayPeriods
            .Select(_mapper.Map<HolidayPeriodDataModel, HolidayPeriod>);
    }

    public async Task<HolidayPlan?> FindHolidayPlanByCollaboratorAsync(Guid collaboratorId)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .Include(hp => hp.HolidayPeriods)
            .SingleOrDefaultAsync();

        if (hpDm == null) return null;

        return _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDm);
    }

    public async Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(PeriodDate periodDate)
    {
        var holidayPlansDMs = await _context.Set<HolidayPlanDataModel>()
                    .Where(hp => hp.HolidayPeriods
                        .Any(hperiod => periodDate.InitDate <= hperiod.PeriodDate.FinalDate
                                     && periodDate.FinalDate >= hperiod.PeriodDate.InitDate))
                    .Include(hp => hp.HolidayPeriods)
                    .ToListAsync();

        return holidayPlansDMs.Select(_mapper.Map<HolidayPlanDataModel, HolidayPlan>);
    }

    public override async Task<IHolidayPlan?> GetByIdAsync(Guid id)
    {
        var hpDM = await _context.Set<HolidayPlanDataModel>().FirstOrDefaultAsync(hp => hp.Id == id);

        if (hpDM == null)
            return null;

        var hp = _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDM);
        return hp;
    }

    public override IHolidayPlan? GetById(Guid id)
    {
        var hpDM = _context.Set<HolidayPlanDataModel>().FirstOrDefault(hp => hp.Id == id);

        if (hpDM == null)
            return null;

        var hp = _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDM);
        return hp;
    }
}