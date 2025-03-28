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

    public int GetDurationInDays(DateOnly start, DateOnly end)
    {
        // Calcula a duração do período de férias dentro do intervalo fornecido
        var effectiveStartDate = _initDate > start ? _initDate : start;
        var effectiveEndDate = _finalDate < end ? _finalDate : end;

        return effectiveEndDate.DayNumber - effectiveStartDate.DayNumber;
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
