using System.Diagnostics;
using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IHolidayPlanRepository : IGenericRepository<IHolidayPlan, IHolidayPeriodVisitor>
{
    public bool AddHolidayPlan(IHolidayPlan holidayPlan);
    public bool CanInsertHolidayPeriod(long holidayPlanId, IHolidayPeriod periodDate);
    public Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(long collaboratorId, IPeriodDate periodDate);
    public Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(IPeriodDate periodDate);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(long collaboratorId, IPeriodDate periodDate, int days);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<long> collabIds, IPeriodDate periodDate);
    public Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(long collaboratorId);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(long collaboratorId);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(long collaboratorId, IPeriodDate period);
    public Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days);
    public bool HolidayPeriodExists(long holidayPlanId, IPeriodDate periodDate);
}
