using System.Diagnostics;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IHolidayPlanRepository : IGenericRepository<IHolidayPlan, IHolidayPeriodVisitor>
{
    public bool CanInsertHolidayPeriod(long holidayPlanId, IHolidayPeriod periodDate);
    public Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(long collaboratorId, PeriodDate periodDate);
    public Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(PeriodDate periodDate);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(long collaboratorId, PeriodDate periodDate, int days);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<long> collabIds, PeriodDate periodDate);
    public Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(long collaboratorId);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(long collaboratorId);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(long collaboratorId, PeriodDate period);
    public Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days);
}
