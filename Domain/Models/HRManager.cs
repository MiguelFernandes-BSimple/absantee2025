using Domain.Interfaces;

namespace Domain.Models;

public class HRManager : IHRManager
{
    private long _id;
    private long _userId;
    public PeriodDateTime _periodDateTime { get; set; }

    public HRManager(long userId, PeriodDateTime periodDateTime)
    {
        _periodDateTime = periodDateTime;
        _userId = userId;
    }

    public HRManager(long id, long userId, PeriodDateTime periodDateTime)
        : this(userId, periodDateTime)
    {
        _id = id;
    }

    public bool ContractContainsDates(PeriodDateTime periodDateTime)
    {
        return _periodDateTime.Contains(periodDateTime);
    }

    public long GetId()
    {
        return _id;
    }

    public long GetUserId()
    {
        return _userId;
    }
}