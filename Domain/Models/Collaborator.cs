using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    public long _id { get; set; }
    public long _userId { get; set; }
    public IPeriodDateTime _periodDateTime { get; set; }

    public Collaborator(long userId, IPeriodDateTime periodDateTime)
    {
        _periodDateTime = periodDateTime;
        _userId = userId;
    }

    public Collaborator(long id, long userId, IPeriodDateTime periodDateTime)
    {
        _id = id;
        _userId = userId;
        _periodDateTime = periodDateTime;
    }
    public long GetId()
    {
        return _id;
    }
    public long GetUserId()
    {
        return _userId;
    }
    public IPeriodDateTime GetPeriodDateTime()
    {
        return _periodDateTime;
    }

    public bool ContractContainsDates(IPeriodDateTime periodDateTime)
    {
        return _periodDateTime.Contains(periodDateTime);
    }
}
