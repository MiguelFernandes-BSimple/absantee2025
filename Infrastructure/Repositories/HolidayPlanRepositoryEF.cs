using Domain.Models;
using Infrastructure.DataModel;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Repositories;
public class HolidayPlanRepositoryEF : GenericRepositoryEF<HolidayPlan, HolidayPlanDataModel>, IHolidayPlanRepository
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
        return await _context.Set<HolidayPlanDataModel>().AnyAsync
            (h => h.Id == holidayPlanId && h.GetHolidayPeriods().Any
                (hp => hp.PeriodDate.InitDate <= periodDate.PeriodDate.InitDate
                    && hp.PeriodDate.FinalDate >= periodDate.PeriodDate.FinalDate));
    }

    public async Task<HolidayPeriod> AddHolidayPeriodAsync(HolidayPeriod holidayPeriod)
    {
        var dataModel = _mapper.Map<HolidayPeriod, HolidayPeriodDataModel>(holidayPeriod);
        _context.Set<HolidayPeriodDataModel>().Add(dataModel);
        await SaveChangesAsync();
        return _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(dataModel);

    }

    public async Task<IEnumerable<HolidayPlan>> FindAllCollaboratorsWithHolidayPeriodsBetweenDatesAsync(PeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.GetHolidayPeriods().Any(h => periodDate.Contains(h.PeriodDate)))
            .Select(hp => _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hp))
            .ToListAsync();
    }

    public async Task<IEnumerable<HolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<Guid> collabIds, PeriodDate periodDate)
    {
        var ret = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => collabIds.Contains(hp.CollaboratorId))
            .SelectMany(hp => hp.HolidayPeriodsDM)
            .Where(hperiod => periodDate.InitDate <= hperiod.PeriodDate.InitDate
                     && periodDate.FinalDate >= hperiod.PeriodDate.FinalDate)
            .ToListAsync();

        return ret.Select(h => _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(h));
    }

    public async Task<IEnumerable<HolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate)
    {

        var holidayPlans = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .Include(hp => hp.HolidayPeriodsDM)
            .ToListAsync();

        var holidayPeriodsDM = holidayPlans
            .SelectMany(hp => hp.HolidayPeriodsDM)
            .Where(hperiod => periodDate.InitDate <= hperiod.PeriodDate.InitDate
                    && periodDate.FinalDate >= hperiod.PeriodDate.FinalDate)
            .ToList();

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
                holidayPlan.HolidayPeriodsDM.Where(h => periodDate.Contains(h.PeriodDate) &&
                                                (periodDate.Duration() > days))
                                          .ToList();

            return ret.Select(h => _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(h));
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<HolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriodsDM
                .Any(h => (h.PeriodDate.FinalDate.DayNumber - h.PeriodDate.InitDate.DayNumber + 1) > days))
            .Include(hp => hp.HolidayPeriodsDM)
            .ToListAsync();

        return hpDm.Select(h => _mapper.Map<HolidayPlanDataModel, HolidayPlan>(h));
    }

    public async Task<IEnumerable<HolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(Guid collaboratorId)
    {
        var holidayPlans = await _context.Set<HolidayPlanDataModel>()
            .FirstOrDefaultAsync(hp => hp.CollaboratorId == collaboratorId);

        if (holidayPlans == null)
            return Enumerable.Empty<HolidayPeriod>();

        return holidayPlans.HolidayPeriodsDM
            .Select(h => _mapper.Map<HolidayPeriodDataModel, HolidayPeriod>(h));
    }

    public async Task<HolidayPlan?> FindHolidayPlanByCollaboratorAsync(Guid collaboratorId)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .Include(hp => hp.HolidayPeriodsDM)
            .SingleOrDefaultAsync();

        if (hpDm == null) return null;

        return _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDm);
    }

    public async Task<IEnumerable<HolidayPlan>> FindHolidayPlansWithinPeriodAsync(PeriodDate periodDate)
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

    public override async Task<HolidayPlan?> GetByIdAsync(Guid id)
    {
        var hpDM = await _context.Set<HolidayPlanDataModel>().FirstOrDefaultAsync(hp => hp.Id == id);

        if (hpDM == null)
            return null;

        var hp = _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDM);
        return hp;
    }

    public override HolidayPlan? GetById(Guid id)
    {
        var hpDM = _context.Set<HolidayPlanDataModel>().FirstOrDefault(hp => hp.Id == id);

        if (hpDM == null)
            return null;

        var hp = _mapper.Map<HolidayPlanDataModel, HolidayPlan>(hpDM);
        return hp;
    }
}