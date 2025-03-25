using Domain;

public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> holidayPlans = new List<IHolidayPlan>();

    public void AddHolidayPlan(IHolidayPlan holidayPlan)
    {
        holidayPlans.Add(holidayPlan);
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(
        IColaborator colaborator,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        // US13 - Como gestor de RH, quero listar os períodos de férias dum colaborador num período
        return holidayPlans
            .Where(h => h.HasColaborator(colaborator))
            .SelectMany(h => h.GetHolidayPeriods())
            .Where(p =>
                (p.GetInitDate() >= initDate && p.GetInitDate() <= endDate)
                || (p.GetFinalDate() >= initDate && p.GetFinalDate() <= endDate)
                || (p.GetInitDate() <= initDate && p.GetFinalDate() >= endDate)
            );
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(
        DateOnly initDate,
        DateOnly endDate
    )
    {
        // US14 - Como gestor de RH, quero listar os colaboradores que têm de férias num período
        return holidayPlans
            .Where(h =>
                h.GetHolidayPeriods()
                    .Any(p =>
                        (p.GetInitDate() >= initDate && p.GetInitDate() <= endDate)
                        || (p.GetFinalDate() >= initDate && p.GetFinalDate() <= endDate)
                        || (p.GetInitDate() <= initDate && p.GetFinalDate() >= endDate)
                    )
            )
            .Select(h => h.GetColaborator())
            .Distinct();
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

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(
        IColaborator colaborator,
        DateOnly initDate,
        DateOnly endDate,
        int days
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorThatIncludeWeekends(
        IColaborator colaborator
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(
        IColaborator colaborator1,
        IColaborator colaborator2,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
        IProject project,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    public int GetHolidayDaysForProjectCollaboratorBetweenDates(
        IAssociationProjectColaborator association,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
        IProject project,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }
}
