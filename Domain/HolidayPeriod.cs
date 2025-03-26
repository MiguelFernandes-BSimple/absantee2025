
namespace Domain;
public class HolidayPeriod : IHolidayPeriod
{
    private DateOnly _initDate;
    private DateOnly _finalDate;

    public HolidayPeriod(DateOnly initDate, DateOnly finalDate)
    {
        if (CheckInputValues(initDate, finalDate))
        {
            _initDate = initDate;
            _finalDate = finalDate;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public DateOnly GetInitDate()
    {
        return _initDate;
    }

    public DateOnly GetFinalDate()
    {
        return _finalDate;
    }

    public int GetDuration()
    {
        return _finalDate.DayNumber - _initDate.DayNumber + 1;
    }

    public bool IsLongerThan(int days)
    {
        if (GetDuration() > days)
            return true;

        return false;
    }

    private bool CheckInputValues(DateOnly initDate, DateOnly endDate)
    {
        if (initDate > endDate)
            return false;

        return true;
    }

    public bool HolidayPeriodOverlap(IHolidayPeriod holidayPeriod)
    {
        return _initDate <= holidayPeriod.GetInitDate()
            && _finalDate >= holidayPeriod.GetFinalDate();
    }

    public int GetNumberOfCommonDaysBetweenPeriods(DateOnly initDate, DateOnly finalDate)
    {
        DateOnly interceptionStart = initDate > _initDate ? initDate : _initDate;

        DateOnly interceptionEnd = finalDate < _finalDate ? finalDate : _finalDate;

        if (interceptionStart <= interceptionEnd)
        {
            return interceptionEnd.DayNumber - interceptionStart.DayNumber + 1;
        }

        return 0;
    }

    public bool ContainsWeekend(DateOnly initDate, DateOnly endDate)
    {
        for (var date = initDate; date <= endDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                return true;
            }
        }
        return false;
    }
}