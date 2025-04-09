using Domain.Interfaces;

namespace Domain.IRepository;

public interface IHolidayPlanRepository
{
    public bool AddHolidayPlan(IHolidayPlan holidayPlan);
    public Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate);
    public IEnumerable<IHolidayPlan> FindHolidayPlansWithinPeriod(IPeriodDate periodDate);
    public Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(IPeriodDate periodDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate, int days);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate, int days);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(List<long> validCollaboratorsIds, IPeriodDate periodDate);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<long> validCollaboratorsIds, IPeriodDate periodDate);
    public IHolidayPlan? FindHolidayPlanByCollaborator(ICollaborator collaborator);
    public Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(ICollaborator collaborator);
    public IHolidayPlan? FindHolidayPlanByCollaborator(long collaboratorId);
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaborator(ICollaborator collaborator);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(ICollaborator collaborator);
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDates(long collaboratorId, IPeriodDate period);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(long collaborator, IPeriodDate period);
    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days);
    public Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days);

}
