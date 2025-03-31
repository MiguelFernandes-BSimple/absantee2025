namespace Domain;

public interface IHolidayPlanRepository
{
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, DateOnly initDate, DateOnly endDate);
    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(DateOnly initDate, DateOnly endDate);
    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days);
    public int GetHolidayDaysOfCollaboratorInProject(IAssociationProjectCollaborator association);
    public IHolidayPeriod? GetHolidayPeriodContainingDate(ICollaborator collaborator, DateOnly date);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, DateOnly initDate, DateOnly endDate, int days);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(ICollaborator collaborator, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(ICollaborator collaborator1, ICollaborator collaborator2, DateOnly initDate, DateOnly endDate);
    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(IEnumerable<ICollaborator> collaborators, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAlltCollaboratorsBetweenDates(List<ICollaborator> validCollaborators, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPlan> FindAll();
}
