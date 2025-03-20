namespace Domain;

public class HolidaysPlan
{
    private List<IHolidayPeriod> _holidaysPeriods;
    private IColaborator _colaborator;

    public HolidaysPlan(IHolidayPeriod holidayPeriods, IColaborator colaborator) :
        this(new List<IHolidayPeriod>() {holidayPeriods}, colaborator)
    {
    }

    public HolidaysPlan(List<IHolidayPeriod> holidaysPeriods, IColaborator colaborator)
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
        if(CanInsertHolidayPeriod(holidayPeriod, this._holidaysPeriods, this._colaborator)){
            _holidaysPeriods.Add(holidayPeriod);
            return true;
        } else
            return false;
    }

    private bool CheckInputValues(List<IHolidayPeriod> periodoFerias, IColaborator colaborador)
    {
        for (int i = 0; i < periodoFerias.Count; i++)
        {
            if (!CanInsertHolidayPeriod(periodoFerias[i], periodoFerias.Skip(i + 1).ToList(), colaborador)){
                return false;
            }
        }
        return true;
    }

    private bool CanInsertHolidayPeriod(IHolidayPeriod holidayPeriod, List<IHolidayPeriod> holidayPeriods, IColaborator colaborator)
    {
        if (!colaborator.IsInside(holidayPeriod.GetInitDate().ToDateTime(TimeOnly.MinValue), holidayPeriod.GetFinalDate().ToDateTime(TimeOnly.MinValue)))
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

    public bool IsSizeList(int size){
        return size == this._holidaysPeriods.Count();
    }
}