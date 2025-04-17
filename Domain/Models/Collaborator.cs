using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    public long _id { get; set; }
    public long _userId { get; set; }
    public PeriodDateTime _periodDateTime { get; set; }

    public Collaborator(long userId, PeriodDateTime periodDateTime)
    {
        _periodDateTime = periodDateTime;
        _userId = userId;
    }

    public Collaborator(long id, long userId, PeriodDateTime periodDateTime)
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

    public bool ContractContainsDates(PeriodDateTime periodDateTime)
    {
        return _periodDateTime.Contains(periodDateTime);
    }
}
