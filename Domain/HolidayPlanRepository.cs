using Domain;

public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> holidayPlans = new List<IHolidayPlan>();
    private readonly IAssociationProjectColaboratorRepository _associationRepo;

    public HolidayPlanRepository(IAssociationProjectColaboratorRepository associationRepo)
    {
        _associationRepo =
            associationRepo ?? throw new ArgumentNullException(nameof(associationRepo));
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(
        IColaborator colaborator,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(
        DateOnly initDate,
        DateOnly endDate
    )
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

    //uc21
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
        IProject project,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        var validCollaborators = _associationRepo.FindAllProjectCollaboratorsBetween(
            project,
            initDate,
            endDate
        );
        return holidayPlans
            .Where(hp => validCollaborators.Contains(hp.GetColaborator()))
            .SelectMany(hp =>
                hp.GetHolidayPeriods()
                    .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
            );
    }

    //uc22
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(
        IAssociationProjectColaborator association,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        if (association.AssociationIntersectDates(initDate, endDate))
        {
            var colaborador = association.GetColaborator();
            var project = association.GetProject();
            var collaboratorHolidayPlan = holidayPlans.FirstOrDefault(hp =>
                hp.GetColaborator().Equals(colaborador)
            );

            if (collaboratorHolidayPlan == null)
                return 0;

            int totalHolidayDays = 0;

            foreach (var holidayColabPeriod in collaboratorHolidayPlan.GetHolidayPeriods())
            {
                DateOnly holidayStart = holidayColabPeriod.GetInitDate();
                DateOnly holidayEnd = holidayColabPeriod.GetFinalDate();

                if (association.AssociationIntersectDates(holidayStart, holidayEnd))
                {
                    totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(
                        holidayStart,
                        holidayEnd
                    );
                }
            }

            return totalHolidayDays;
        }
        return 0;
    }

    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
        IProject project,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        throw new NotImplementedException();
    }

    public void AddHolidayPlan(IHolidayPlan holidayPlan)
    {
        holidayPlans.Add(holidayPlan);
    }
}
