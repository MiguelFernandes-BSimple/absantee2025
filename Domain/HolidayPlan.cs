namespace Domain;

public class HolidayPlan : IHolidayPlan
{
    private List<IHolidayPeriod> _holidaysPeriods;
    private IColaborator _colaborator;

    public HolidayPlan(IHolidayPeriod holidayPeriod, IColaborator colaborator) :
        this(new List<IHolidayPeriod>() { holidayPeriod }, colaborator)
    {
    }

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

    public IColaborator GetCollaborator()
    {
        return _colaborator;
    }

    public int GetNumberOfHolidayDaysBetween(DateOnly initDate, DateOnly endDate)
    {
        return _holidaysPeriods.Sum(period => period.GetNumberOfCommonDaysBetweenPeriods(initDate, endDate));
    }



    private bool CheckInputValues(List<IHolidayPeriod> periodoFerias, IColaborator colaborador)
    {
        for (int i = 0; i < periodoFerias.Count; i++)
        {
            if (!CanInsertHolidayPeriod(periodoFerias[i], periodoFerias.Skip(i + 1).ToList(), colaborador))
            {
                return false;
            }
        }
        return true;
    }

    private bool CanInsertHolidayPeriod(IHolidayPeriod holidayPeriod, List<IHolidayPeriod> holidayPeriods, IColaborator colaborator)
    {
        DateTime holidayPeriodInitDate = holidayPeriod.GetInitDate().ToDateTime(TimeOnly.MinValue);
        DateTime holidayPeriodFinalDate = holidayPeriod.GetFinalDate().ToDateTime(TimeOnly.MinValue);
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
}