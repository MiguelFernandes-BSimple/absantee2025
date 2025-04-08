using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    private long _id;
    private long _userId;
    private IUser _user;
    private IPeriodDateTime _periodDateTime;

    public Collaborator(IUser user, IPeriodDateTime periodDateTime)
    {
        if (CheckInputFields(user, periodDateTime))
        {
            _periodDateTime = periodDateTime;
            _user = user;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public Collaborator(long userId, IUser user, DateTime initDate) :
        this(user, new PeriodDateTime(initDate, DateTime.MaxValue))
    {
        _userId = userId;
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
    public IUser GetUser()
    {
        return _user;
    }
    public IPeriodDateTime GetPeriodDateTime()
    {
        return _periodDateTime;
    }

    private bool CheckInputFields(IUser user, IPeriodDateTime periodDateTime)
    {
        if (user.DeactivationDateIsBefore(periodDateTime.GetFinalDate()))
            return false;

        if (user.IsDeactivated())
            return false;

        return true;
    }

    public bool ContractContainsDates(IPeriodDateTime periodDateTime)
    {
        return _periodDateTime.Contains(periodDateTime);
    }

    public bool HasNames(string names)
    {
        return _user.HasNames(names);
    }

    public bool HasSurnames(string surnames)
    {
        return _user.HasSurnames(surnames);
    }

    override public bool Equals(Object? obj)
    {
        if (obj == null) return false;

        if (obj.GetType() == typeof(Collaborator))
        {
            Collaborator other = (Collaborator)obj;
            if (_user.Equals(other._user)
                && _periodDateTime.Intersects(other._periodDateTime))
                return true;
        }

        return false;
    }
}
