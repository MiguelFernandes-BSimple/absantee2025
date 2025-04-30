using Domain.Models;

namespace Domain.Interfaces;

public interface IHolidayPlan
{
    Guid Id { get; }
    Guid CollaboratorId { get; }
    List<HolidayPeriod> HolidayPeriods { get; }
    int GetNumberOfHolidayDaysBetween(PeriodDate periodDate);
}
