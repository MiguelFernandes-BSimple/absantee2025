using Domain.Interfaces;

namespace Domain.Models;

public class ProjectManager : IProjectManager
{
    private long _id;
    private long _userId;
    private IPeriodDateTime _periodDateTime;

    public ProjectManager(long userId, IPeriodDateTime periodDateTime)
    {
        _userId = userId;
        _periodDateTime = periodDateTime;
    }

    public ProjectManager(long userId, DateTime initDate)
    {
        _userId = userId;
        _periodDateTime = new PeriodDateTime(initDate, DateTime.MaxValue);
    }

    public long GetId()
    {
        return _id;
    }

    public void SetId(long id)
    {
        _id = id;
    }
    public long GetUserId()
    {
        return _userId;
    }

    public IPeriodDateTime GetPeriodDateTime()
    {
        return _periodDateTime;
    }
}