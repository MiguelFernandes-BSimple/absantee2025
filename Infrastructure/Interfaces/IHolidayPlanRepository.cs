using Domain.Interfaces;

namespace Infrastructure.Interfaces;

public interface IHolidayPlanRepository
{
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate);
    public IEnumerable<IHolidayPlan> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(IPeriodDate periodDate);
    public IEnumerable<IHolidayPlan> FindAllCollaboratorsWithHolidayPeriodsBetweenDatesAsync(IPeriodDate periodDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate, int days);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate, int days);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(List<ICollaborator> validCollaborators, IPeriodDate periodDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<ICollaborator> validCollaborators, IPeriodDate periodDate);
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaborator(ICollaborator collaborator);
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaboratorAsync(ICollaborator collaborator);
    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days);
    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThanAsync(int days);
    public IHolidayPlan? FindHolidayPlanByCollaborator(ICollaborator collaborator);
    public IHolidayPlan? FindHolidayPlanByCollaboratorAsync(ICollaborator collaborator);
    public bool AddHolidayPlan(IHolidayPlan holidayPlan);
    public bool AddHolidayPlanAsync(IHolidayPlan holidayPlan);
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate period);
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate period);

}
