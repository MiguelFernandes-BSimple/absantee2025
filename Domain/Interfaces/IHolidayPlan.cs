using Domain.Models;

namespace Domain.Interfaces;

public interface IHolidayPlan
{
    long GetId();
    List<IHolidayPeriod> GetHolidayPeriods();
    long GetCollaboratorId();
    int GetNumberOfHolidayDaysBetween(PeriodDate periodDate);
}
