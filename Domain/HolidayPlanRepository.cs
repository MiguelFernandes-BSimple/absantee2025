using Domain;
public class HolidayPlanRepository : IHolidayPlanRepository
{
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsForCollaboratorBetween(IColaborator colaborator, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsBetween(DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days)
    {
        throw new NotImplementedException();
    }

    public int GetHolidayDaysInProject(IProject project)
    {
        throw new NotImplementedException();
    }

    public IHolidayPeriod GetHolidayPeriodContainingDate(IColaborator colaborator, DateOnly date)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsForCollaboratorBetweenLongerThan(IColaborator colaborator, DateOnly initDate, DateOnly endDate, int days)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsForCollaboratorThatIncludeWeekends(IColaborator colaborator)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindOverlappingHolidayPeriodsForCollaboratorsBetween(IColaborator colaborator1, IColaborator colaborator2, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsForProjectCollaboratorsBetween(IProject project, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public int GetHolidayDaysForProjectCollaboratorBetween(IAssociationProjectColaborator association, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public int GetHolidayDaysForAllProjectCollaboratorsBetween(IProject project, DateOnly initDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }
}
