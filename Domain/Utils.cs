namespace Domain;

public static class Utils
{
    public static bool ContainsWeekend(DateOnly initDate, DateOnly endDate)
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

    public static DateOnly DataMax(DateOnly initDate, DateOnly endDate)
    {
        return initDate > endDate ? initDate : endDate;
    }

    public static DateOnly DataMin(DateOnly initDate, DateOnly endDate)
    {
        return initDate < endDate ? initDate : endDate;
    }

}
