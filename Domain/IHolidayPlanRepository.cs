namespace Domain;

public interface IHolidayPlanRepository
{
    public IEnumerable<IHolidayPlan> FindAll();
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, DateOnly initDate, DateOnly endDate);
    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(DateOnly initDate, DateOnly endDate);
    public IHolidayPeriod? GetHolidayPeriodContainingDate(ICollaborator collaborator, DateOnly date);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, DateOnly initDate, DateOnly endDate, int days);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(ICollaborator collaborator, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(ICollaborator collaborator1, ICollaborator collaborator2, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(IProject project, DateOnly initDate, DateOnly endDate);
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(IAssociationProjectCollaborator association, DateOnly initDate, DateOnly endDate);
    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(IEnumerable<ICollaborator> collaborators, DateOnly initDate, DateOnly endDate);

}
