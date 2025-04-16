using Domain.Factory;
using Domain.Interfaces;

namespace Domain.Models;

public class HolidayPlan : IHolidayPlan
{
    private long _id;
    private long _collaboratorId;
    private List<IHolidayPeriod> _holidaysPeriods;

    public HolidayPlan(long collaboratorId, List<IHolidayPeriod> holidayPeriods)
    {
        this._collaboratorId = collaboratorId;
        this._holidaysPeriods = holidayPeriods;
    }

    public HolidayPlan(long id, long collaboratorId, List<IHolidayPeriod> holidayPeriods)
    {
        this._id = id;
        this._collaboratorId = collaboratorId;
        this._holidaysPeriods = holidayPeriods;

    }

    public long GetId()
    {
        return _id;
    }

    public long GetCollaboratorId()
    {
        return _collaboratorId;
    }

    public List<IHolidayPeriod> GetHolidayPeriods()
    {
        return new List<IHolidayPeriod>(_holidaysPeriods);
    }

    public int GetNumberOfHolidayDaysBetween(IPeriodDate periodDate)
    {
        return _holidaysPeriods.Sum(period =>
            period.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate)
        );
    }

}