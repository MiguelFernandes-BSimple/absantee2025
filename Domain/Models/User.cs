using Domain.Interfaces;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Domain.Models;

public class User : IUser
{
    private long _id;
    private string _names;
    private string _surnames;
    private string _email;
    public PeriodDateTime _periodDateTime;

    public User(string names, string surnames, string email, DateTime? deactivationDate)
    {

        Regex nameRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,50}$");

        if (!nameRegex.IsMatch(names) || !nameRegex.IsMatch(surnames))
            throw new ArgumentException("Names or surnames are invalid.");

        try
        {
            var emailValidator = new MailAddress(email);
        }
        catch (Exception)
        {
            throw new ArgumentException("Email is invalid.");
        }

        deactivationDate ??= DateTime.MaxValue;

        if (DateTime.Now >= deactivationDate)
            throw new ArgumentException("Deactivaton date can't be in the past.");

        _names = names;
        _surnames = surnames;
        _email = email;
        _periodDateTime = new PeriodDateTime(DateTime.Now, (DateTime)deactivationDate);
    }

    public User(long id, string names, string surnames, string email, PeriodDateTime periodDateTime)
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

    public PeriodDateTime GetPeriodDateTime()
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
