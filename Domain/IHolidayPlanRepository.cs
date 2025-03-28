namespace Domain;

public interface IHolidayPlanRepository
{
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, DateOnly initDate, DateOnly endDate);
    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(DateOnly initDate, DateOnly endDate);
    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days);
    public int GetHolidayDaysOfCollaboratorInProject(IAssociationProjectCollaborator association);
    public int GetHolidayDaysInProject(IProject project);
    public IHolidayPeriod? GetHolidayPeriodContainingDate(ICollaborator collaborator, DateOnly date);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, DateOnly initDate, DateOnly endDate, int days);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorThatIncludeWeekends(ICollaborator collaborator);
    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(ICollaborator collaborator1, ICollaborator collaborator2, DateOnly searchInitDate, DateOnly searchEndDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(IProject project, DateOnly initDate, DateOnly endDate);
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(IAssociationProjectCollaborator association, DateOnly initDate, DateOnly endDate);
    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(IEnumerable<ICollaborator> collaborators, DateOnly initDate, DateOnly endDate);

}