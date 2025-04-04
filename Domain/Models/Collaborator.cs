using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    private IUser _user;
    private IPeriodDateTime _periodDateTime;

    public Collaborator(IUser user, IPeriodDateTime periodDateTime)
    {
        if (CheckInputFields(user, periodDateTime))
        {
            this._periodDateTime = periodDateTime;
            this._user = user;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public Collaborator(IUser user, DateTime initDate) : 
        this(user, new PeriodDateTime(initDate, DateTime.MaxValue))
    {
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
