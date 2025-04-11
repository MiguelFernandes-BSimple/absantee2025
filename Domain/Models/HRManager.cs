using Domain.Interfaces;

namespace Domain.Models;

public class HRManager : IHRManager
{
    private long _id;
    private long _userId;
    private IPeriodDateTime _periodDateTime;

    public HRManager(long userId, IPeriodDateTime periodDateTime)
    {
        _periodDateTime = periodDateTime;
        _userId = userId;
    }

    public HRManager(long userId, DateTime initDate) 
    {
        
        _userId = userId;
        _periodDateTime = new PeriodDateTime(initDate, DateTime.MaxValue);
    }

    public bool ContractContainsDates(IPeriodDateTime periodDateTime)
    {
        return _periodDateTime.Contains(periodDateTime);
    }

    public long GetId()
    {
        return _id;
    }

    public IPeriodDateTime GetPeriodDateTime()
    {
        return _periodDateTime;
    }

    public long GetUserId()
    {
        return _userId;
    }

     override public bool Equals(Object? obj)
    {
        if (obj == null) return false;

        if (obj.GetType() == typeof(HRManager))
        {
            HRManager other = (HRManager)obj;
            if (_userId.Equals(other._userId)
                && _periodDateTime.Intersects(other._periodDateTime))
                return true;
        }

        return false;
    }
}