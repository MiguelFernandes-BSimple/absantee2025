namespace Infrastructure.Repositories;
using Infrastructure.Interfaces;
using Domain.Interfaces;
using Domain;

public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> _holidayPlans = new List<IHolidayPlan>();

    public HolidayPlanRepository()
    {
        _holidayPlans = new List<IHolidayPlan>();
    }

    public HolidayPlanRepository(List<IHolidayPlan> holidayPlans) : this()
    {
        bool isValid = true;

        //Validate if holidayPlan list is valid
        // -> All holidayPlans have to be valid
        for (int hpIndex1 = 0; hpIndex1 < holidayPlans.Count; hpIndex1++)
        {
            if (!isValid)
                break;

            IHolidayPlan currHolidayPlan = holidayPlans[hpIndex1];
            isValid = CanInsert(currHolidayPlan, holidayPlans.Skip(hpIndex1 + 1).ToList());
        }

        // If the list is valid -> insert hlidayPlans in repo
        if (isValid)
        {
            foreach (IHolidayPlan hpIndex2 in holidayPlans)
            {
                AddHolidayPlan(hpIndex2);
            }
        }
        else
        {
            throw new ArgumentException("Arguments are not valid!");
        }
    }

    private bool IsHolidayPeriodValid(IHolidayPeriod period, DateOnly initDate, DateOnly endDate)
    {
        return period.GetInitDate() <= endDate && period.GetFinalDate() >= initDate;
    }

    public IEnumerable<IHolidayPlan> GetHolidayPlansWithHolidayPeriodValid(DateOnly initDate, DateOnly endDate)
    {
        return _holidayPlans.Where(h => h.GetHolidayPeriods().Any(p => IsHolidayPeriodValid(p, initDate, endDate)));
    }

    // US13 - Como gestor de RH, quero listar os períodos de férias dum collaborador num período
    // * todo - colocar in period
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(
        ICollaborator collaborator,
        DateOnly initDate,
        DateOnly endDate
    )
    {
        // isto deve ser verificado dentro de uma classe period, que ainda está por criar
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
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(
                List<ICollaborator> validCollaborators,
                DateOnly initDate,
                DateOnly endDate
            )
    {

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
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaborator(
            ICollaborator collaborator
        )
    {
        return _holidayPlans.FirstOrDefault(hp =>
            hp.GetCollaborator().Equals(collaborator))?.GetHolidayPeriods() ?? new List<IHolidayPeriod>();
    }

    public IEnumerable<IHolidayPlan> GetHolidayPlansByAssociations(IAssociationProjectCollaborator association)
    {
        var collaborator = association.GetCollaborator();

        return _holidayPlans
            .Where(hp => hp.GetCollaborator().Equals(collaborator));
    }

    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlans.Where(p => p.HasPeriodLongerThan(days));
    }

    public IHolidayPlan? FindHolidayPlanByCollaborator(ICollaborator collaborator)
    {
        return _holidayPlans.SingleOrDefault(p => p.GetCollaborator() == collaborator);
    }

    /**
    * Method to validate whether a holidayPlan can be insert in a given list or not
    * -> There can't be multiple holidayPlans for a single collaborator
    * It's one or none
    */
    private bool CanInsert(IHolidayPlan holidayPlan, List<IHolidayPlan> holidayPlansList)
    {
        bool alreadyExists =
            holidayPlansList.Any(hp => hp.GetCollaborator().Equals(holidayPlan.GetCollaborator()));

        return !alreadyExists;
    }

    /**
    * Method to add a single holidayPlan to the repository
    */
    public bool AddHolidayPlan(IHolidayPlan holidayPlan)
    {
        bool canInsert = CanInsert(holidayPlan, _holidayPlans);

        if (canInsert)
            _holidayPlans.Add(holidayPlan);

        return canInsert;
    }
}