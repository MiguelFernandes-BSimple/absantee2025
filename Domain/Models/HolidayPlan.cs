using Domain.Interfaces;
namespace Domain.Models;

public class HolidayPlan : IHolidayPlan
{
    private List<IHolidayPeriod> _holidaysPeriods;
    private ICollaborator _collaborator;

    public HolidayPlan(IHolidayPeriod holidayPeriod, ICollaborator collaborator)
        : this(new List<IHolidayPeriod>() { holidayPeriod }, collaborator) { }

    public HolidayPlan(List<IHolidayPeriod> holidaysPeriods, ICollaborator collaborator)
    {
        if (CheckInputValues(holidaysPeriods, collaborator))
        {
            this._holidaysPeriods = new List<IHolidayPeriod>(holidaysPeriods);
            this._collaborator = collaborator;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public bool AddHolidayPeriod(IHolidayPeriod holidayPeriod)
    {
        if (CanInsertHolidayPeriod(holidayPeriod, this._holidaysPeriods, this._collaborator))
        {
            _holidaysPeriods.Add(holidayPeriod);
            return true;
        }
        else
            return false;
    }

    public int GetNumberOfHolidayDaysBetween(IPeriodDate periodDate)
    {
        return _holidaysPeriods.Sum(period =>
            period.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate)
        );
    }

    public bool HasPeriodLongerThan(int days)
    {
        return _holidaysPeriods.Any(period => period.IsLongerThan(days));
    }

    private bool CheckInputValues(List<IHolidayPeriod> periodoFerias, ICollaborator collaborador)
    {
        for (int i = 0; i < periodoFerias.Count; i++)
        {
            if (
                !CanInsertHolidayPeriod(
                    periodoFerias[i],
                    periodoFerias.Skip(i + 1).ToList(),
                    collaborador
                )
            )
            {
                return false;
            }
        }
        return true;
    }

    private bool CanInsertHolidayPeriod(
        IHolidayPeriod holidayPeriod,
        List<IHolidayPeriod> holidayPeriods,
        ICollaborator collaborator
    )
    {
        IPeriodDate period = holidayPeriod.GetPeriodDate();
        PeriodDateTime periodDateTime = new PeriodDateTime(period);

        if (!collaborator.ContractContainsDates(periodDateTime))
            return false;

        foreach (IHolidayPeriod pf in holidayPeriods)
        {
            if (holidayPeriod.Contains(pf))
                return false;
        }
        return true;
    }

    public List<IHolidayPeriod> GetHolidayPeriods()
    {
        return [.. _holidaysPeriods];
    }

    public int GetDurationInDays(IPeriodDate periodDate)
    {
        return _holidaysPeriods.Sum(hp => hp.GetInterceptionDurationInDays(periodDate));
    }

    // métodos utilizados no holiday plan repository
    public bool HasCollaborator(ICollaborator collaborator)
    {
        if (collaborator.Equals(_collaborator))
            return true;
        return false;
    }

    public ICollaborator GetCollaborator()
    {
        // Este método retorna uma referencia do objeto uma vez que, para implementar uma cópia,
        // seriam necessários métodos auxiliares no collaborador e user.
        // pelo que vi, existem outras alternativas, mas também implicam algumas modificações:
        // https://www.reddit.com/r/csharp/comments/uc81wl/create_a_copy_of_an_object/
        return _collaborator;
    }

    public IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date)
    {
        return _holidaysPeriods.Where(a => a.ContainsDate(date)).FirstOrDefault();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(
        IPeriodDate period,
        int days
    )
    {
        return _holidaysPeriods.Where(a => a.Contains(period) && a.GetDuration() > days);
    }

    public IEnumerable<IHolidayPeriod> GetHolidayPeriodsBetweenPeriod(IPeriodDate period)
    {
        return _holidaysPeriods.Where(hperiod => hperiod.Intersects(period));
    }

    public bool HasIntersectingHolidayPeriod(IPeriodDate period)
    {
        return _holidaysPeriods.Any(hperiod => hperiod.Intersects(period));
    }

    public int GetNumberOfHolidayPeriods()
    {
        return _holidaysPeriods.Count;
    }
}
