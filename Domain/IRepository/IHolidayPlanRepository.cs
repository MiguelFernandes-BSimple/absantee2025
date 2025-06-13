using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IHolidayPlanRepository : IGenericRepositoryEF<IHolidayPlan, HolidayPlan, IHolidayPeriodVisitor>
{
    public Task<bool> CanInsertHolidayPlan(Guid collaboratorId);
    public Task<bool> CanInsertHolidayPeriod(Guid holidayPlanId, HolidayPeriod periodDate);
    public Task<HolidayPeriod> AddHolidayPeriodAsync(Guid holidayPlanId, HolidayPeriod holidayPeriod);
    public Task<HolidayPeriod> UpdateHolidayPeriodAsync(Guid collabId, HolidayPeriod holidayPeriod);
    public Task<IEnumerable<HolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate);
    public Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(PeriodDate periodDate);
    public Task<IEnumerable<HolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(Guid collaboratorId, PeriodDate periodDate, int days);
    Task<IEnumerable<HolidayPeriod>> FindHolidayPeriodsByCollaboratorIntersectingPeriodDate(Guid collaboratorId, PeriodDate periodDate);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<Guid> collabIds, PeriodDate periodDate);
    Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsIntersectingPeriodAsync(List<Guid> collabIds, PeriodDate periodDate);
    public Task<HolidayPlan?> FindHolidayPlanByCollaboratorAsync(Guid collaboratorId);
    public Task<IEnumerable<HolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(Guid collaboratorId);
    public Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days);
}
