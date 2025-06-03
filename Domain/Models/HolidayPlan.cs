using Domain.Interfaces;

namespace Domain.Models;

public class HolidayPlan : IHolidayPlan
{
    public Guid Id { get; }
    public Guid CollaboratorId { get; }
    public List<IHolidayPeriod> HolidayPeriods { get; }

    public HolidayPlan(Guid collaboratorId, List<IHolidayPeriod> holidayPeriods)
    {
        Id = Guid.NewGuid();
        CollaboratorId = collaboratorId;
        HolidayPeriods = holidayPeriods;
    }

    public HolidayPlan(Guid id, Guid collaboratorId, List<IHolidayPeriod> holidayPeriods)
    {
        Id = id;
        CollaboratorId = collaboratorId;
        HolidayPeriods = holidayPeriods;
    }

    public int GetNumberOfHolidayDaysBetween(PeriodDate periodDate)
    {
        return HolidayPeriods.Sum(period =>
            period.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate)
        );
    }

}