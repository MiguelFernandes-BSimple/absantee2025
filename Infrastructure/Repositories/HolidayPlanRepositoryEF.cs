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
public class HolidayPlanRepositoryEF : GenericRepository<IHolidayPlan>, IHolidayPlanRepository
{
    private HolidayPlanMapper mapper;
    public HolidayPlanRepositoryEF(AbsanteeContext context) : base(context)
    {
        mapper = new HolidayPlanMapper();
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
            _context.Set<HolidayPlanDataModel>().Add(mapper.ToDataModel((HolidayPlan) holidayPlan));
            _context.SaveChanges();
        }

        return canInsert;
    }

    public async Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan)
    {
        bool canInsert = await CanInsertAsync(holidayPlan);

        if (canInsert)
        {
            await _context.Set<HolidayPlanDataModel>().AddAsync(mapper.ToDataModel((HolidayPlan) holidayPlan));
            await _context.SaveChangesAsync();
        }

        return canInsert;
    }

    public IEnumerable<IHolidayPlan> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(IPeriodDate periodDate)
    {
        return _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriods.Any(h => periodDate.Contains(h.GetPeriodDate())))
            .Select(hp => mapper.ToDomain(hp))
            .ToList();
    }

    public async Task<IEnumerable<IHolidayPlan>> FindAllCollaboratorsWithHolidayPeriodsBetweenDatesAsync(IPeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriods.Any(h => periodDate.Contains(h.GetPeriodDate())))
            .Select(hp => mapper.ToDomain(hp))
            .ToListAsync();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(IPeriodDate periodDate)
    {
        return _context.Set<HolidayPlanDataModel>()
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()))
            .ToList();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(IPeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()))
            .ToListAsync();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate)
    {
        return _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()))
            .ToList();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()))
            .ToListAsync();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate, int days)
    {
        return _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()) && (periodDate.Duration() > days))
            .ToList();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate, int days)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SelectMany(hp => hp.HolidayPeriods)
            .Where(h => periodDate.Contains(h.GetPeriodDate()) && (periodDate.Duration() > days))
            .ToListAsync();
    }

    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        var hpDm = _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriods.Any(h => h.IsLongerThan(days)))
            .ToList();

        return mapper.ToDomain(hpDm);
    }

    public async Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.HolidayPeriods.Any(h => h.IsLongerThan(days)))
            .ToListAsync();

        return mapper.ToDomain(hpDm);
    }

    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaborator(ICollaborator collaborator)
    {
        return _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SelectMany(hp => hp.HolidayPeriods)
            .ToList();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(ICollaborator collaborator)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SelectMany(hp => hp.HolidayPeriods)
            .ToListAsync();
    }

    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate period)
    {
        return _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SelectMany(hp => hp.HolidayPeriods.Where(h => h.Intersects(period)))
            .ToList();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate period)
    {
        return await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SelectMany(hp => hp.HolidayPeriods.Where(hp => hp.Intersects(period)))
            .ToListAsync();
    }

    public IHolidayPlan? FindHolidayPlanByCollaborator(ICollaborator collaborator)
    {
        var hpDm = _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SingleOrDefault();

        if (hpDm == null) return null;

        return mapper.ToDomain(hpDm);
    }

    public async Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(ICollaborator collaborator)
    {
        var hpDm = await _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaborator.GetId())
            .SingleOrDefaultAsync();

        if (hpDm == null) return null;

        return mapper.ToDomain(hpDm);
    }

    public IEnumerable<IHolidayPlan> FindHolidayPlansWithinPeriod(IPeriodDate periodDate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(IPeriodDate periodDate)
    {
        throw new NotImplementedException();
    }

    public IHolidayPlan? FindHolidayPlanByCollaborator(long collaboratorId)
    {
        var hpDm = _context.Set<HolidayPlanDataModel>()
            .Where(hp => hp.CollaboratorId == collaboratorId)
            .SingleOrDefault();

        if (hpDm == null) return null;

        return mapper.ToDomain(hpDm);
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(List<long> validCollaborators, IPeriodDate periodDate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDates(long collaborator, IPeriodDate period)
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
}