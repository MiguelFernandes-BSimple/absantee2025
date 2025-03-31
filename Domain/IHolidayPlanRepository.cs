namespace Domain;

public interface IHolidayPlanRepository
{
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, DateOnly initDate, DateOnly endDate);
    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days);
    public int GetHolidayDaysOfCollaboratorInProject(IAssociationProjectCollaborator association);
    public IHolidayPeriod? GetHolidayPeriodContainingDate(ICollaborator collaborator, DateOnly date);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, DateOnly initDate, DateOnly endDate, int days);
    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(IEnumerable<ICollaborator> collaborators, DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(List<ICollaborator> validCollaborators, DateOnly initDate, DateOnly endDate);
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaborator(ICollaborator collaborator);
    public IEnumerable<IHolidayPlan> GetHolidayPlansWithHolidayPeriodValid(DateOnly initDate, DateOnly endDate);
    public IEnumerable<IHolidayPlan> GetHolidayPlansByAssociations(IAssociationProjectCollaborator association);

}
