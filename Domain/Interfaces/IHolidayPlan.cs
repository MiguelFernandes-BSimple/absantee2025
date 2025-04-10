namespace Domain.Interfaces;

public interface IHolidayPlan
{
    long GetId();
    bool AddHolidayPeriod(IPeriodDate periodDate);
    List<IHolidayPeriod> GetHolidayPeriods();
    int GetDurationInDays(IPeriodDate periodDate);
    IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date);
    IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(IPeriodDate periodDate, int days);
    long GetCollaboratorId();
    int GetNumberOfHolidayDaysBetween(IPeriodDate periodDate);
    bool HasPeriodLongerThan(int days);
    IEnumerable<IHolidayPeriod> GetHolidayPeriodsBetweenPeriod(IPeriodDate period);
    bool HasIntersectingHolidayPeriod(IPeriodDate period);
    bool HasCollaboratorId(long collabId);
}
