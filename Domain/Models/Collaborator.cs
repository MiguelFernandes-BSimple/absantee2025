using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    private long _id;
    private long _userId;
    private IPeriodDateTime _periodDateTime;

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

    public void SetId(long id)
    {
        _id = id;
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


    override public bool Equals(Object? obj)
    {
        if (obj == null) return false;

        if (obj.GetType() == typeof(Collaborator))
        {
            Collaborator other = (Collaborator)obj;
            if (_userId.Equals(other._userId)
                && _periodDateTime.Intersects(other._periodDateTime))
                return true;
        }

        return false;
    }
}
