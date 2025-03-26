
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

    private bool CheckInputValues(DateOnly dataInicio, DateOnly dataFim)
    {
        if (dataInicio > dataFim)
            return false;

        return true;
    }

    public bool HolidayPeriodOverlap(IHolidayPeriod holidayPeriod)
    {
        return _initDate <= holidayPeriod.GetInitDate()
            && _finalDate >= holidayPeriod.GetFinalDate();
    }

    public int GetNumberOfCommonUtilDaysBetweenPeriods(DateOnly initDate, DateOnly finalDate)
    {
        DateOnly interceptionStart = initDate > _initDate ? initDate : _initDate;

        DateOnly interceptionEnd = finalDate < _finalDate ? finalDate : _finalDate;

        if (interceptionStart <= interceptionEnd)
        {
            int weekdayCount = 0;

            for (DateOnly date = interceptionStart; date <= interceptionEnd; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekdayCount++;
                }
            }
            return weekdayCount;
        }

        return 0;
    }
}