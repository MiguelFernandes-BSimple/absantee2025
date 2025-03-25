namespace Domain;

public class HolidayPlan : IHolidayPlan
{
    private List<IHolidayPeriod> _holidaysPeriods;
    private IColaborator _colaborator;

    public HolidayPlan(IHolidayPeriod holidayPeriod, IColaborator colaborator)
        : this(new List<IHolidayPeriod>() { holidayPeriod }, colaborator) { }

    public HolidayPlan(List<IHolidayPeriod> holidaysPeriods, IColaborator colaborator)
    {
        if (CheckInputValues(holidaysPeriods, colaborator))
        {
            this._holidaysPeriods = new List<IHolidayPeriod>(holidaysPeriods);
            this._colaborator = colaborator;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public bool AddHolidayPeriod(IHolidayPeriod holidayPeriod)
    {
        if (CanInsertHolidayPeriod(holidayPeriod, this._holidaysPeriods, this._colaborator))
        {
            _holidaysPeriods.Add(holidayPeriod);
            return true;
        }
        else
            return false;
    }

    private bool CheckInputValues(List<IHolidayPeriod> periodoFerias, IColaborator colaborador)
    {
        for (int i = 0; i < periodoFerias.Count; i++)
        {
            if (
                !CanInsertHolidayPeriod(
                    periodoFerias[i],
                    periodoFerias.Skip(i + 1).ToList(),
                    colaborador
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
        IColaborator colaborator
    )
    {
        DateTime holidayPeriodInitDate = holidayPeriod.GetInitDate().ToDateTime(TimeOnly.MinValue);
        DateTime holidayPeriodFinalDate = holidayPeriod
            .GetFinalDate()
            .ToDateTime(TimeOnly.MinValue);
        if (!colaborator.ContainsDates(holidayPeriodInitDate, holidayPeriodFinalDate))
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
    public bool HasColaborator(IColaborator colaborator)
    {
        if (colaborator.Equals(_colaborator))
            return true;
        return false;
    }

    public IEnumerable<IHolidayPeriod> GetHolidayPeriods()
    {
        // Retorna uma cópia da lista para evitar modificações externas
        return new List<IHolidayPeriod>(_holidaysPeriods);
    }

    public IColaborator GetColaborator()
    {
        // Este método retorna uma referencia do objeto uma vez que, para implementar uma cópia,
        // seriam necessários métodos auxiliares no colaborador e user.
        // pelo que vi, existem outras alternativas, mas também implicam algumas modificações:
        // https://www.reddit.com/r/csharp/comments/uc81wl/create_a_copy_of_an_object/
        return _colaborator;
    }
}
