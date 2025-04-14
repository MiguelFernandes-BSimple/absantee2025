using Domain.Interfaces;

namespace Domain.Models;

public class User : IUser
{
    private long _id;
    private string _names;
    private string _surnames;
    private string _email;
    private IPeriodDateTime _periodDateTime;

    public User(string names, string surnames, string email, DateTime? deactivationDate)
    {
        deactivationDate ??= DateTime.MaxValue;

        _names = names;
        _surnames = surnames;
        _email = email;
        _periodDateTime = new PeriodDateTime(DateTime.Now, (DateTime)deactivationDate);
    }

    public User(long id, string names, string surnames, string email, IPeriodDateTime periodDateTime)
    {
        _id = id;
        _names = names;
        _surnames = surnames;
        _email = email;
        _periodDateTime = periodDateTime;

    }

    public long GetId()
    {
        return _id;
    }

    public void SetId(long id)
    {
        _id = id;
    }

    public string GetNames()
    {
        return _names;
    }

    public string GetSurnames()
    {
        return _surnames;
    }

    public string GetEmail()
    {
        return _email;
    }

    public IPeriodDateTime GetPeriodDateTime()
    {
        return _periodDateTime;
    }

    public bool IsDeactivated()
    {
        return _periodDateTime.IsFinalDateSmallerThan(DateTime.Now);
    }

    public bool DeactivationDateIsBefore(DateTime date)
    {
        return _periodDateTime.IsFinalDateSmallerThan(date);
    }

    public bool DeactivateUser()
    {
        if (IsDeactivated())
            return false;

        _periodDateTime.SetFinalDate(DateTime.Now);

        return true;
    }

    public bool HasNames(string names)
    {
        if (string.IsNullOrWhiteSpace(names))
            return false;

        return _names.Contains(names, StringComparison.OrdinalIgnoreCase);
    }

    public bool HasSurnames(string surnames)
    {
        if (string.IsNullOrWhiteSpace(surnames))
            return false;

        return _surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase);
    }

    /**
    * As of now, two users are the same if the email is the same
    */
    override public bool Equals(Object? obj)
    {
        if (obj == null) return false;

        if (obj.GetType() == typeof(User))
        {
            User other = (User)obj;

            if (this._email.Equals(other._email))
                return true;
        }

        return false;
    }
}
