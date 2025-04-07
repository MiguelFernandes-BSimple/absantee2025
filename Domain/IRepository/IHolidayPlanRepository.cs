using Domain.Interfaces;

namespace Domain.IRepository;

public interface IHolidayPlanRepository
{
    public bool AddHolidayPlan(IHolidayPlan holidayPlan);
    public Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate);
    public IEnumerable<IHolidayPlan> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(IPeriodDate periodDate);
    public Task<IEnumerable<IHolidayPlan>> FindAllCollaboratorsWithHolidayPeriodsBetweenDatesAsync(IPeriodDate periodDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate, int days);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate, int days);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(List<ICollaborator> validCollaborators, IPeriodDate periodDate);
    public Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<ICollaborator> validCollaborators, IPeriodDate periodDate);
    public IHolidayPlan? FindHolidayPlanByCollaborator(ICollaborator collaborator);
    public Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(ICollaborator collaborator);
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaborator(ICollaborator collaborator);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(ICollaborator collaborator);
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate period);
    public Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate period);
    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days);
    public Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days);

}
