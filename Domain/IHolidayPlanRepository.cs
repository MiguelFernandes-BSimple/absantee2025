using Domain;

public interface IHolidayPlanRepository
{
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(IColaborator colaborator, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(DateOnly initDate, DateOnly endDate);
    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days);
    public int GetHolidayDaysInProject(IProject project);
    public IEnumerable<IHolidayPeriod> GetHolidayPeriodContainingDate(IColaborator colaborator, DateOnly date);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(IColaborator colaborator, DateOnly initDate, DateOnly endDate, int days);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorThatIncludeWeekends(IColaborator colaborator);
    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(IColaborator colaborator1, IColaborator colaborator2, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(IProject project, DateOnly initDate, DateOnly endDate);
    //DUVIDA ASSOCIAR?
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(IAssociationProjectColaborator association, DateOnly initDate, DateOnly endDate);
    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(IProject project, DateOnly initDate, DateOnly endDate);

}