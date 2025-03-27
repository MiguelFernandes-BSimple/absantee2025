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
        _holidayPlans = new List<IHolidayPlan>() { holidayPlan };
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
        // US13 - Como gestor de RH, quero listar os períodos de férias dum collaborador num período
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

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(
        DateOnly initDate,
        DateOnly endDate
    )
    {
        // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
        if (initDate > endDate)
        {
            return Enumerable.Empty<ICollaborator>();
        }
        else
        {
            return _holidayPlans
                .Where(h =>
                    h.GetHolidayPeriods().Any(p => IsHolidayPeriodValid(p, initDate, endDate))
                )
                .Select(h => h.GetCollaborator())
                .Distinct();
        }
    }

    public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlans
            .Where(p => p.HasPeriodLongerThan(days))
            .Select(p => p.GetCollaborator())
            .Distinct();
    }

    public int GetHolidayDaysOfCollaboratorInProject(IAssociationProjectCollaborator association)
    {
        int numberOfHolidayDays = 0;

        IHolidayPlan? collaboratorHolidayPlan = _holidayPlans.SingleOrDefault(p =>
            p.GetCollaborator() == association.GetCollaborator()
        );

        if (collaboratorHolidayPlan == null)
            return 0;

        numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
            association.GetInitDate(),
            association.GetFinalDate()
        );

        return numberOfHolidayDays;
    }

    public IHolidayPeriod? GetHolidayPeriodContainingDate(ICollaborator collaborator, DateOnly date)
    {
        return _holidayPlans.Where(a => a.HasCollaborator(collaborator))
                .Select(a => a.GetHolidayPeriodContainingDate(date)).FirstOrDefault();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(
        ICollaborator collaborator,
        DateOnly initDate,
        DateOnly endDate,
        int days
    )
    {
        return _holidayPlans.Where(a => a.HasCollaborator(collaborator))
                .SelectMany(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(initDate, endDate, days));
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorThatIncludeWeekends(
        ICollaborator collaborator
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(
        ICollaborator collaborator1,
        ICollaborator collaborator2,
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
        IAssociationProjectCollaborator association,
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
    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
    IEnumerable<ICollaborator> collaborators,
    DateOnly initDate,
    DateOnly endDate)
    {
        int totalHolidayDays = 0;

        foreach (var collaborator in collaborators)
        {
            var holidayPeriods = _holidayPlans
                .Where(hp => hp.GetCollaborator().Equals(collaborator))
                .SelectMany(hp => hp.GetHolidayPeriods()
                    .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
                );

            totalHolidayDays += holidayPeriods.Sum(hp => hp.GetDurationInDays(initDate, endDate));
        }

        return totalHolidayDays;
    }
}
