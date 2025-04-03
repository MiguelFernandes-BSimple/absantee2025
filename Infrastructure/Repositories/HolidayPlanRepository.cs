namespace Infrastructure.Repositories;
using Infrastructure.Interfaces;
using Domain.Interfaces;
using Domain;
using Domain.Models;

public class HolidayPlanRepository : IHolidayPlanRepository
{
    private List<IHolidayPlan> _holidayPlans;

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

    // US13 - Como gestor de RH, quero listar os períodos de férias dum collaborador num período
    // * todo - colocar in period
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate)
    {
        var holidayPlan = _holidayPlans.FirstOrDefault(h => h.HasCollaborator(collaborator));
        return holidayPlan?.GetHolidayPeriodsBetweenPeriod(periodDate) ?? Enumerable.Empty<IHolidayPeriod>();
    }


    public IEnumerable<IHolidayPlan> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(IPeriodDate periodDate)
    {
        // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
        return _holidayPlans
            .Where(h =>
                h.HasIntersectingHolidayPeriod(periodDate));
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(
        ICollaborator collaborator,
        IPeriodDate periodDate,
        int days
    )
    {
        return _holidayPlans
            .Where(a => a.HasCollaborator(collaborator))
            .SelectMany(a =>
                a.FindAllHolidayPeriodsBetweenDatesLongerThan(periodDate, days)
            );
    }

    //uc21
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(
                List<ICollaborator> collaborators,
                IPeriodDate periodDate
            )
    {

        {
            return _holidayPlans
                .Where(hp => collaborators.Contains(hp.GetCollaborator()))
                .SelectMany(hp =>
                    hp.GetHolidayPeriodsBetweenPeriod(periodDate)
                );
        }

    }

    //uc22
    public List<IHolidayPeriod> FindHolidayPeriodsByCollaborator(
            ICollaborator collaborator
        )
    {
        return _holidayPlans.FirstOrDefault(hp =>
            hp.HasCollaborator(collaborator))?.GetHolidayPeriods() ?? new List<IHolidayPeriod>();
    }

    public List<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDates(
            ICollaborator collaborator, IPeriodDate period
        )
    {
        return _holidayPlans
            .FirstOrDefault(hp => hp.HasCollaborator(collaborator))?.GetHolidayPeriodsBetweenPeriod(period).ToList() ?? new List<IHolidayPeriod>();
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
            holidayPlansList.Any(hp => hp.HasCollaborator(holidayPlan.GetCollaborator()));

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