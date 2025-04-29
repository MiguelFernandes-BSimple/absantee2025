using Domain.Models;
using Infrastructure.DataModel;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using AutoMapper;

namespace Infrastructure.Repositories;
public class HolidayPlanRepositoryEF : GenericRepositoryEF<IHolidayPlan, HolidayPlanDataModel>, IHolidayPlanRepository
{
    private IMapper _mapper;

    public HolidayPlanRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public bool CanInsertHolidayPeriod(Guid holidayPlanId, IHolidayPeriod periodDate)
    {
        return _context.Set<HolidayPlanDataModel>().Any
            (h => h.Id == holidayPlanId && h.GetHolidayPeriods().Any
                (hp => hp.PeriodDate.InitDate <= periodDate.PeriodDate.InitDate
                    && hp.PeriodDate.FinalDate >= periodDate.PeriodDate.FinalDate));
    }

    private async Task<bool> CanInsertAsync(IHolidayPlan holidayPlan)
    {
        return !await _context.Set<HolidayPlanDataModel>().AnyAsync(hp => hp.Id == holidayPlan.CollaboratorId);
    }


    public async Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan)
    {
        bool canInsert = await CanInsertAsync(holidayPlan);

        if (canInsert)
        {
            await _context.Set<HolidayPlanDataModel>().AddAsync(_mapper.Map<HolidayPlan, HolidayPlanDataModel>((HolidayPlan)holidayPlan));
        }

        return canInsert;
    }

    public async Task<IEnumerable<IHolidayPlan>> FindAllCollaboratorsWithHolidayPeriodsBetweenDatesAsync(PeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.GetHolidayPeriods().Any(h => periodDate.Contains(h.PeriodDate)))
            .Select(hp => _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hp))
            .ToListAsync();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<Guid> collabIds, PeriodDate periodDate)
    {
        var ret = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => collabIds.Contains(hp.CollaboratorId))
            .SelectMany(hp => hp.HolidayPeriodsDM)
            .Where(hperiod => periodDate.InitDate <= hperiod.PeriodDate.InitDate
                     && periodDate.FinalDate >= hperiod.PeriodDate.FinalDate)
            .ToListAsync();

        return ret.Select(h => _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(h));
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .SelectMany(hp => hp.GetHolidayPeriods())
            .Where(hperiod => periodDate.InitDate >= hperiod.PeriodDate.InitDate
                           && periodDate.FinalDate <= hperiod.PeriodDate.FinalDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate, int days)
    {
        try
        {
            HolidayPlanDataModel? holidayPlan =
                await _context.Set<HolidayPlanDataModel>()
                              .FirstOrDefaultAsync(hp => hp.CollaboratorId == collaboratorId);

            if (holidayPlan == null)
                return new List<IHolidayPeriod>();

            List<IHolidayPeriod> result =
                holidayPlan.GetHolidayPeriods().Where(h => periodDate.Contains(h.PeriodDate) &&
                                                (periodDate.Duration() > days))
                                          .ToList();

            return result;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriodsDM
                .Any(h => (h.PeriodDate.FinalDate.DayNumber - h.PeriodDate.InitDate.DayNumber + 1) > days))
            .Include(hp => hp.HolidayPeriodsDM)
            .ToListAsync();

        return hpDm.Select(h => _mapper.Map<HolidayPlanDataModel, HolidayPlan>(h));
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(Guid collaboratorId)
    {
        var holidayPlans = await _context.Set<HolidayPlanDataModel>()
            .FirstOrDefaultAsync(hp => hp.CollaboratorId == collaboratorId);

        if (holidayPlans == null)
            return Enumerable.Empty<IHolidayPeriod>();

        return holidayPlans.GetHolidayPeriods();
    }

    public async Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(Guid collaboratorId)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .Include(hp => hp.HolidayPeriodsDM)
            .SingleOrDefaultAsync();

        if (hpDm == null) return null;

        return _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDm);
    }

    public async Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(PeriodDate periodDate)
    {
        var holidayPlansDMs = await _context.Set<HolidayPlanDataModel>()
                    .Where(hp => hp.HolidayPeriodsDM
                        .Any(hperiod => periodDate.InitDate <= hperiod.PeriodDate.InitDate
                                    && periodDate.FinalDate >= hperiod.PeriodDate.FinalDate))
                    // include adicionado com o prof, sendo que hplan e hperiod são um agregado
                    .Include(hp => hp.HolidayPeriodsDM)
                    .ToListAsync();

        return holidayPlansDMs.Select(h => _mapper.Map<HolidayPlanDataModel, HolidayPlan>(h));
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