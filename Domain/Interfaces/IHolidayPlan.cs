namespace Domain.Interfaces;

public interface IHolidayPlan
{
    long GetId();
    List<IHolidayPeriod> GetHolidayPeriods();
    long GetCollaboratorId();
    int GetNumberOfHolidayDaysBetween(IPeriodDate periodDate);
}
