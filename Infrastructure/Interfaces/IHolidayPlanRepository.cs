using Domain.Interfaces;

namespace Infrastructure.Interfaces;

public interface IHolidayPlanRepository
{
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate);
    public IEnumerable<IHolidayPlan> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(IPeriodDate periodDate);
    public IHolidayPeriod? GetHolidayPeriodContainingDate(ICollaborator collaborator, DateOnly date);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate, int days);
    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(IEnumerable<ICollaborator> collaborators, IPeriodDate periodDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(List<ICollaborator> validCollaborators, IPeriodDate periodDate);
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaborator(ICollaborator collaborator);
    public IEnumerable<IHolidayPlan> GetHolidayPlansWithHolidayPeriodValid(IPeriodDate periodDate);
    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days);
    public IHolidayPlan? FindHolidayPlanByCollaborator(ICollaborator collaborator);
    public bool AddHolidayPlan(IHolidayPlan holidayPlan);
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate period);

}
