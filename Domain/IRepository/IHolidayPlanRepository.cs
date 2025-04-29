using System.Diagnostics;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IHolidayPlanRepository : IGenericRepository<IHolidayPlan, IHolidayPeriodVisitor>
{
    public bool CanInsertHolidayPeriod(Guid holidayPlanId, IHolidayPeriod periodDate);
    public Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate);
    public Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(PeriodDate periodDate);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate, int days);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<Guid> collabIds, PeriodDate periodDate);
    public Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(Guid collaboratorId);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(Guid collaboratorId);
    public Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days);
}
