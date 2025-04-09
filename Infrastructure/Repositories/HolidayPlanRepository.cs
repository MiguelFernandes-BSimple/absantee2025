namespace Infrastructure.Repositories;
using Domain.IRepository;
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

    public async Task<bool> AddHolidayPlanAsync(IHolidayPlan holidayPlan)
    {
        var result = AddHolidayPlan(holidayPlan);
        return await Task.FromResult(result);
    }

    // US13 - Como gestor de RH, quero listar os períodos de férias dum collaborador num período
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate)
    {
        var holidayPlan = _holidayPlans.FirstOrDefault(h => h.HasCollaborator(collaborator));
        return holidayPlan?.GetHolidayPeriodsBetweenPeriod(periodDate) ?? Enumerable.Empty<IHolidayPeriod>();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate)
    {
        var result = FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator, periodDate);
        return await Task.FromResult(result);
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForCollaboratorBetweenDates(long collaboratorId, IPeriodDate periodDate)
    {
        var holidayPlan = _holidayPlans.FirstOrDefault(h => h.HasCollaboratorId(collaboratorId));
        return holidayPlan?.GetHolidayPeriodsBetweenPeriod(periodDate) ?? Enumerable.Empty<IHolidayPeriod>();
    }

    public IEnumerable<IHolidayPlan> FindHolidayPlansWithinPeriod(IPeriodDate periodDate)
    {
        return _holidayPlans
            .Where(h => h.HasIntersectingHolidayPeriod(periodDate));
    }

    public async Task<IEnumerable<IHolidayPlan>> FindHolidayPlansWithinPeriodAsync(IPeriodDate periodDate)
    {
        var result = FindHolidayPlansWithinPeriod(periodDate);
        return await Task.FromResult(result);
    }


    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(ICollaborator collaborator, IPeriodDate periodDate, int days)
    {
        return _holidayPlans
            .Where(a => a.HasCollaborator(collaborator))
            .SelectMany(a =>
                a.FindAllHolidayPeriodsBetweenDatesLongerThan(periodDate, days)
            );
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(ICollaborator collaborator, IPeriodDate periodDate, int days)
    {
        var result = FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(collaborator, periodDate, days);
        return await Task.FromResult(result);
    }


    //uc21
    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(List<long> collaborators, IPeriodDate periodDate)
    {
        return _holidayPlans
            .Where(hp => collaborators.Contains(hp.GetCollaboratorId()))
            .SelectMany(hp => hp.GetHolidayPeriodsBetweenPeriod(periodDate));
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(List<long> collaborators, IPeriodDate periodDate)
    {
        var result = FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collaborators, periodDate);
        return await Task.FromResult(result);
    }


    public IHolidayPlan? FindHolidayPlanByCollaborator(ICollaborator collaborator)
    {
        return _holidayPlans.SingleOrDefault(p => p.HasCollaborator(collaborator));
    }

    public async Task<IHolidayPlan?> FindHolidayPlanByCollaboratorAsync(ICollaborator collaborator)
    {
        var result = FindHolidayPlanByCollaborator(collaborator);
        return await Task.FromResult(result);
    }

    public IHolidayPlan? FindHolidayPlanByCollaborator(long collaboratorId)
    {
        return _holidayPlans.SingleOrDefault(p => p.GetCollaboratorId() == collaboratorId);
    }

    //uc22
    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaborator(ICollaborator collaborator)
    {
        return _holidayPlans.FirstOrDefault(hp =>
            hp.HasCollaborator(collaborator))?.GetHolidayPeriods() ?? Enumerable.Empty<IHolidayPeriod>();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorAsync(ICollaborator collaborator)
    {
        var result = FindHolidayPeriodsByCollaborator(collaborator);
        return await Task.FromResult(result);
    }


    public IEnumerable<IHolidayPeriod> FindHolidayPeriodsByCollaboratorBetweenDates(long collaborator, IPeriodDate periodDate)
    {
        return _holidayPlans.FirstOrDefault(hp =>
            hp.HasCollaboratorId(collaborator))?.GetHolidayPeriodsBetweenPeriod(periodDate) ?? Enumerable.Empty<IHolidayPeriod>();
    }

    public async Task<IEnumerable<IHolidayPeriod>> FindHolidayPeriodsByCollaboratorBetweenDatesAsync(long collaborator, IPeriodDate periodDate)
    {
        var result = FindHolidayPeriodsByCollaboratorBetweenDates(collaborator, periodDate);
        return await Task.FromResult(result);
    }


    public IEnumerable<IHolidayPlan> FindAllWithHolidayPeriodsLongerThan(int days)
    {
        return _holidayPlans.Where(p => p.HasPeriodLongerThan(days));
    }


    public async Task<IEnumerable<IHolidayPlan>> FindAllWithHolidayPeriodsLongerThanAsync(int days)
    {
        var result = FindAllWithHolidayPeriodsLongerThan(days);
        return await Task.FromResult(result);
    }
}