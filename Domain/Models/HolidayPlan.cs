using Domain.Factory;
using Domain.Interfaces;

namespace Domain.Models;

public class HolidayPlan : IHolidayPlan
{
    private long _id;
    private long _collaboratorId;
    private List<IHolidayPeriod> _holidaysPeriods;

    public HolidayPlan(long collaboratorId)
    {
        this._collaboratorId = collaboratorId;
        this._holidaysPeriods = new List<IHolidayPeriod>();
    }

    public HolidayPlan(long id, long collaboratorId)
    {
        this._id = id;
        this._collaboratorId = collaboratorId;
        this._holidaysPeriods = new List<IHolidayPeriod>();
    }

    public long GetId()
    {
        return _id;
    }

    public void SetId(long id)
    {
        _id = id;
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

    public bool HasPeriodLongerThan(int days)
    {
        return _holidaysPeriods.Any(period => period.IsLongerThan(days));
    }

    public int GetDurationInDays(IPeriodDate periodDate)
    {
        return _holidaysPeriods.Sum(hp => hp.GetInterceptionDurationInDays(periodDate));
    }

    // métodos utilizados no holiday plan repository
    public bool HasCollaborator(long collaboratorId)
    {
        if (collaboratorId.Equals(_collaboratorId))
            return true;

        return false;
    }

    public bool HasCollaboratorId(long collabId)
    {
        if (_collaboratorId == collabId)
            return true;

        return false;
    }

    public IHolidayPeriod? GetHolidayPeriodContainingDate(DateOnly date)
    {
        return _holidaysPeriods.Where(a => a.ContainsDate(date)).FirstOrDefault();
    }

    public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsBetweenDatesLongerThan(IPeriodDate period, int days)
    {
        return _holidaysPeriods.Where(a => a.Contains(period) && a.GetDuration() > days);
    }

    public IEnumerable<IHolidayPeriod> GetHolidayPeriodsBetweenPeriod(IPeriodDate period)
    {
        return _holidaysPeriods.Where(hperiod => hperiod.Intersects(period));
    }

    public bool HasIntersectingHolidayPeriod(IPeriodDate period)
    {
        return _holidaysPeriods.Any(hperiod => hperiod.Intersects(period));
    }
}