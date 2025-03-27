using System.IO.Compression;
using Domain;

public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> _holidayPlans = new List<IHolidayPlan>();

    public HolidayPlanRepository(List<IHolidayPlan> holidayPlans)
    {
        _holidayPlans = holidayPlans;
    }
    public HolidayPlanRepository(IHolidayPlan holidayPlan)
    {
        _holidayPlans = new List<IHolidayPlan>(){ holidayPlan };
    }
    private bool IsHolidayPeriodValid(IHolidayPeriod period, DateOnly initDate, DateOnly endDate)
    {
        return period.GetInitDate() <= endDate && period.GetFinalDate() >= initDate;
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(
        ICollaborator collaborator,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        // US13 - Como gestor de RH, quero listar os períodos de férias dum colaborador num período
        if (initDate > endDate)
        {
            return Enumerable.Empty<IHolidayPeriod>();
        }
        else
        {
            return _holidayPlans
                .Where(h => h.HasCollaborator(collaborator))
                .SelectMany(h => h.GetHolidayPeriods())
                .Where(p => IsHolidayPeriodValid(p, initDate, endDate));
        }
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(
        DateOnly initDate,
        DateOnly endDate
    )
    {
        // US14 - Como gestor de RH, quero listar os colaboradores que têm de férias num período
        if (initDate > endDate)
        {
            return Enumerable.Empty<IColaborator>();
        }
        else
        {
            return _holidayPlans
                .Where(h =>
                    h.GetHolidayPeriods().Any(p => IsHolidayPeriodValid(p, initDate, endDate))
                )
                .Select(h => h.GetColaborator())
                .Distinct();
        }
    }

    public IEnumerable<IColaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlans
            .Where(p => p.HasPeriodLongerThan(days))
            .Select(p => p.GetColaborator())
            .Distinct();
    }

    public int GetHolidayDaysOfCollaboratorInProject(IAssociationProjectColaborator association)
    {
        int numberOfHolidayDays = 0;

        IHolidayPlan? collaboratorHolidayPlan = _holidayPlans.SingleOrDefault(p =>
            p.GetColaborator() == association.GetColaborator()
        );

        if (collaboratorHolidayPlan == null)
            return 0;

        numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
            association.GetInitDate(),
            association.GetFinalDate()
        );

        return numberOfHolidayDays;
    }

    public IHolidayPeriod? GetHolidayPeriodContainingDate(IColaborator colaborator, DateOnly date)
    {
        return _holidayPlans.Where(a => a.HasCollaborator(colaborator))
                .Select(a => a.GetHolidayPeriodContainingDate(date)).FirstOrDefault();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(
        IColaborator colaborator,
        DateOnly initDate,
        DateOnly endDate,
        int days
    )
    {
        return _holidayPlans.Where(a => a.HasCollaborator(colaborator))
                .SelectMany(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(initDate, endDate, days));
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

    public int GetHolidayDaysInProject(IProject project)
    {
        throw new NotImplementedException();
    }
}
