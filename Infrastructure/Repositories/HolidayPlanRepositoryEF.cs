using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DataModel;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infrastructure.Mapper;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class HolidayPlanRepositoryEF : GenericRepository<IHolidayPlan, HolidayPlanDataModel>, IHolidayPlanRepository
{    
    private HolidayPlanMapper _mapper;
    public HolidayPlanRepositoryEF(AbsanteeContext context, HolidayPlanMapper mapper) : base(context, (IMapper<IHolidayPlan, HolidayPlanDataModel>) mapper)
    {
        _mapper = mapper;
    }

    private bool CanInsert(IHolidayPlan holidayPlan)
    {
        return _context.Set<HolidayPlanDataModel>().Any(hp => hp.Id == holidayPlan.GetCollaboratorId());
    }

    public bool CanInsertHolidayPeriod(long holidayPlanId, IHolidayPeriod periodDate)
    {
        return _context.Set<HolidayPlanDataModel>().Any
            (h => h.Id == holidayPlanId && h.HolidayPeriods.Any
                (hp => hp.GetPeriodDate().GetInitDate() <= periodDate.GetPeriodDate().GetInitDate()
                    && hp.GetPeriodDate().GetFinalDate() >= periodDate.GetPeriodDate().GetFinalDate()));
    }

    private async Task<bool> CanInsertAsync(IHolidayPlan holidayPlan)
    {
        return !await _context.Set<HolidayPlanDataModel>().AnyAsync(hp => hp.Id == holidayPlan.GetCollaboratorId());
    }

    public bool AddHolidayPlan(IHolidayPlan holidayPlan)
    {
        bool canInsert = CanInsert(holidayPlan);

        if (canInsert)
        {
            _context.Set<HolidayPlanDataModel>().Add(_mapper.ToDataModel((HolidayPlan) holidayPlan));
            _context.SaveChanges();
        }

        return canInsert;
    }

    public async Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan)
    {
        bool canInsert = await CanInsertAsync(holidayPlan);

        if (canInsert)
        {
            await _context.Set<HolidayPlanDataModel>().AddAsync(_mapper.ToDataModel((HolidayPlan) holidayPlan));
            await _context.SaveChangesAsync();
        }

        return canInsert;
    }

    public async Task<IEnumerable<IHolidayPlan>> FindAllCollaboratorsWithHolidayPeriodsBetweenDatesAsync(IPeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriods.Any(h => periodDate.Contains(h.GetPeriodDate())))
            .Select(hp => _mapper.ToDomain(hp))
            .ToListAsync();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<long> collabIds, IPeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => collabIds.Contains(hp.CollaboratorId))
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()))
            .ToListAsync();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(long collaboratorId, IPeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()))
            .ToListAsync();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(long collaboratorId, IPeriodDate periodDate, int days)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()) && (periodDate.Duration() > days))
            .ToListAsync();
    }

    public async Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriods.Any(h => h.IsLongerThan(days)))
            .ToListAsync();

        return _mapper.ToDomain(hpDm);
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(long collaboratorId)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .SelectMany(hp => hp.HolidayPeriods)
            .ToListAsync();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(long collaboratorId, IPeriodDate period)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .SelectMany(hp => hp.HolidayPeriods.Where(hp => hp.Intersects(period)))
            .ToListAsync();
    }

    public async Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(long collaboratorId)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .SingleOrDefaultAsync();

        if (hpDm == null) return null;

        return _mapper.ToDomain(hpDm);
    }

    public Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(IPeriodDate periodDate)
    {
        throw new NotImplementedException();
    }

    public bool HolidayPeriodExists(long holidayPlanId, IPeriodDate periodDate)
    {
        var holidayPlan = GetById(holidayPlanId);

        if (holidayPlan == null)
            return false;

        var result = holidayPlan.GetHolidayPeriods().Where(hp => hp.GetPeriodDate() == periodDate);

        if (result == null)
            return false;

        return true;
    }

    public override IHolidayPlan? GetById(long id)
    {
        var hpDM = _context.Set<HolidayPlanDataModel>().FirstOrDefault(hp => hp.Id == id);

        if (hpDM == null)
            return null;

        var hp = _mapper.ToDomain(hpDM);
        return hp;
    }

    public override async Task<IHolidayPlan?> GetByIdAsync(long id)
    {
        var hpDM = await _context.Set<HolidayPlanDataModel>().FirstOrDefaultAsync(hp => hp.Id == id);

        if (hpDM == null)
            return null;

        var hp = _mapper.ToDomain(hpDM);
        return hp;
    }
}