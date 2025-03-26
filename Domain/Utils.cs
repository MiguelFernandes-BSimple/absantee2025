public static class DateUtils
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

}
