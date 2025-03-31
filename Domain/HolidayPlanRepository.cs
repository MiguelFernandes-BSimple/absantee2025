using System.IO.Compression;
using Domain;

public class HolidayPlanRepository : IHolidayPlanRepository
{
    private readonly IAssociationProjectCollaboratorRepository? _associationRepo;
    private List<IHolidayPlan> _holidayPlans = new List<IHolidayPlan>();

    public HolidayPlanRepository(List<IHolidayPlan> holidayPlans)
    {
        _holidayPlans = holidayPlans;
    }

    public HolidayPlanRepository(IHolidayPlan holidayPlan)
    {
        _holidayPlans = new List<IHolidayPlan>() { holidayPlan };
    }

    public HolidayPlanRepository(IAssociationProjectCollaboratorRepository associationRepo)
    {
        _associationRepo = associationRepo;
    }

    public HolidayPlanRepository(
        IAssociationProjectCollaboratorRepository associationRepo,
        IHolidayPlan holidayPlan
    )
    {
        _holidayPlans = new List<IHolidayPlan>() { holidayPlan };
        _associationRepo = associationRepo;
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

    public IHolidayPeriod? GetHolidayPeriodContainingDate(ICollaborator collaborator, DateOnly date)
    {
        return _holidayPlans
            .Where(a => a.HasCollaborator(collaborator))
            .Select(a => a.GetHolidayPeriodContainingDate(date))
            .FirstOrDefault();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(
        ICollaborator collaborator,
        DateOnly initDate,
        DateOnly endDate,
        int days
    )
    {
        return _holidayPlans
            .Where(a => a.HasCollaborator(collaborator))
            .SelectMany(a =>
                a.FindAllHolidayPeriodsBetweenDatesLongerThan(initDate, endDate, days)
            );
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(
        ICollaborator collaborator,
        DateOnly searchInitDate,
        DateOnly searchEndDate
    )
    {
        if (!Utils.ContainsWeekend(searchInitDate, searchEndDate))
            return Enumerable.Empty<IHolidayPeriod>();

        IEnumerable<IHolidayPeriod> holidayPeriodsBetweenDates =
            FindAllHolidayPeriodsForCollaboratorBetweenDates(
                collaborator,
                searchInitDate,
                searchEndDate
            );

        IEnumerable<IHolidayPeriod> hp = holidayPeriodsBetweenDates.Where(holidayPeriod =>
        {
            DateOnly intersectionStart = Utils.DataMax(holidayPeriod.GetInitDate(), searchInitDate);
            DateOnly intersectionEnd = Utils.DataMin(holidayPeriod.GetFinalDate(), searchEndDate);
            return intersectionStart <= intersectionEnd
                && Utils.ContainsWeekend(intersectionStart, intersectionEnd);
        });

        return hp;
    }

    public IEnumerable<IHolidayPeriod> FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(
        ICollaborator collaborator1,
        ICollaborator collaborator2,
        DateOnly searchInitDate,
        DateOnly searchEndDate
    )
    {
        IEnumerable<IHolidayPeriod> holidayPeriodListColab1 =
            FindAllHolidayPeriodsForCollaboratorBetweenDates(
                collaborator1,
                searchInitDate,
                searchEndDate
            );
        IEnumerable<IHolidayPeriod> holidayPeriodListColab2 =
            FindAllHolidayPeriodsForCollaboratorBetweenDates(
                collaborator2,
                searchInitDate,
                searchEndDate
            );

        return holidayPeriodListColab1
            .SelectMany(period1 =>
                holidayPeriodListColab2
                    .Where(period2 =>
                        period1.GetInitDate() <= period2.GetFinalDate()
                        && period1.GetFinalDate() >= period2.GetInitDate()
                    )
                    .SelectMany(period2 => new List<IHolidayPeriod> { period1, period2 })
            )
            .Distinct();
    }

    public int GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
        IEnumerable<ICollaborator> collaborators,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        int totalHolidayDays = 0;
        foreach (var collaborator in collaborators)
        {
            var holidayPeriods = _holidayPlans
                .Where(hp => hp.GetCollaborator().Equals(collaborator))
                .SelectMany(hp =>
                    hp.GetHolidayPeriods()
                        .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
                );

            totalHolidayDays += holidayPeriods.Sum(hp => hp.GetDurationInDays(initDate, endDate));
        }

        return totalHolidayDays;
    }

    //uc21
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
        IProject project,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        if (_associationRepo == null)
        {
            throw new Exception();
        }

        var validCollaborators = _associationRepo.FindAllProjectCollaboratorsBetween(
            project,
            initDate,
            endDate
        );
        if (validCollaborators == null || initDate > endDate)
        {
            return Enumerable.Empty<IHolidayPeriod>();
        }
        else
        {
            return _holidayPlans
                .Where(hp => validCollaborators.Contains(hp.GetCollaborator()))
                .SelectMany(hp =>
                    hp.GetHolidayPeriods()
                        .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
                );
        }
    }

    //uc22
    public int GetHolidayDaysForProjectCollaboratorBetweenDates(
        IAssociationProjectCollaborator association,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        if (initDate > endDate)
        {
            return 0;
        }
        if (association.AssociationIntersectDates(initDate, endDate))
        {
            var colaborador = association.GetCollaborator();
            var project = association.GetProject();
            var collaboratorHolidayPlan = _holidayPlans.FirstOrDefault(hp =>
                hp.GetCollaborator().Equals(colaborador)
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

    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlans.Where(p => p.HasPeriodLongerThan(days));
    }

    public IHolidayPlan? FindHolidayPlanByAssociationProjectCollaborator(IAssociationProjectCollaborator association)
    {
        return _holidayPlans.SingleOrDefault(p => p.GetCollaborator() == association.GetCollaborator());
    }
}
