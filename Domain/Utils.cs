namespace Domain;

public static class Utils
{
    public static bool ContainsWeekend(DateOnly initDate, DateOnly endDate)
    {
        if (initDate > endDate)
            throw new Exception("The start date can't be after the end date.");
            
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
