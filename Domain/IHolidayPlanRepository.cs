using Domain;

public interface IHolidayPlanRepository
{
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsForCollaboratorBetween(IColaborator colaborator, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsBetween(DateOnly initDate, DateOnly endDate);
    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days);
    public int GetHolidayDaysInProject(IProject project);
    public IHolidayPeriod GetHolidayPeriodContainingDate(IColaborator colaborator, DateOnly date);
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsForCollaboratorBetweenLongerThan(IColaborator colaborator, DateOnly initDate, DateOnly endDate, int days);
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsForCollaboratorThatIncludeWeekends(IColaborator colaborator);
    public IEnumerable<IHolidayPeriod> FindOverlappingHolidayPeriodsForCollaboratorsBetween(IColaborator colaborator1, IColaborator colaborator2, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsForProjectCollaboratorsBetween(IProject project, DateOnly initDate, DateOnly endDate);
    //DUVIDA ASSOCIAR?
    public int GetHolidayDaysForProjectCollaboratorBetween(IAssociationProjectColaborator association, DateOnly initDate, DateOnly endDate);
    public int GetHolidayDaysForAllProjectCollaboratorsBetween(IProject project, DateOnly initDate, DateOnly endDate);

}