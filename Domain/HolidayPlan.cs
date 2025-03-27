namespace Domain;

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

    public int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate)
    {
        return _holidaysPeriods.Sum(period =>
            period.GetNumberOfCommonUtilDaysBetweenPeriods(initDate, endDate)
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
        DateTime holidayPeriodInitDate = holidayPeriod.GetInitDate().ToDateTime(TimeOnly.MinValue);
        DateTime holidayPeriodFinalDate = holidayPeriod
            .GetFinalDate()
            .ToDateTime(TimeOnly.MinValue);
        if (!collaborator.ContractContainsDates(holidayPeriodInitDate, holidayPeriodFinalDate))
            return false;
        foreach (IHolidayPeriod pf in holidayPeriods)
        {
            if (holidayPeriod.HolidayPeriodOverlap(pf))
            {
                return false;
            }
        }
        return true;
    }

    // métodos utilizados no holiday plan repository
    public bool HasCollaborator(ICollaborator collaborator)
    {
        if (collaborator.Equals(_collaborator))
            return true;
        return false;
    }

    public IEnumerable<IHolidayPeriod> GetHolidayPeriods()
    {
        // Retorna uma cópia da lista para evitar modificações externas
        return new List<IHolidayPeriod>(_holidaysPeriods);
    }

    public ICollaborator GetCollaborator()
    {
        // Este método retorna uma referencia do objeto uma vez que, para implementar uma cópia,
        // seriam necessários métodos auxiliares no collaborador e user.
        // pelo que vi, existem outras alternativas, mas também implicam algumas modificações:
        // https://www.reddit.com/r/csharp/comments/uc81wl/create_a_copy_of_an_object/
        return _collaborator;
    }

    public IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date) {
        return _holidaysPeriods.Where(a => a.ContainsDate(date)).FirstOrDefault();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(DateOnly ini, DateOnly end, int days) {
        return _holidaysPeriods.Where(a => a.ContainedBetween(ini, end) && a.GetDuration() > days);
    }
}
