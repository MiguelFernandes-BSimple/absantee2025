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

    public HRManager(long id, long userId, IPeriodDateTime periodDateTime)
    {
        _id = id;
        _userId = userId;
        _periodDateTime = periodDateTime;
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
}