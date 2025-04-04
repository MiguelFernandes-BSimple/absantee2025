using System.Net.Mail;
using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models;

public class User : IUser
{
    private string _names;
    private string _surnames;
    private string _email;
    private IPeriodDateTime _periodDateTime;

    public User(string names, string surnames, string email, DateTime? deactivationDate)
    {
        deactivationDate ??= DateTime.MaxValue;

        if (CheckInputValues(names, surnames, email, deactivationDate))
        {
            _names = names;
            _surnames = surnames;
            _email = email;
            _periodDateTime = new PeriodDateTime(DateTime.Now, (DateTime)deactivationDate);
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(string names, string surnames, string email, DateTime? deactivationDate)
    {
        Regex nameRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,50}$");

        if (!nameRegex.IsMatch(names) || !nameRegex.IsMatch(surnames))
            return false;

        try
        {
            var emailValidator = new MailAddress(email);
        }
        catch (Exception)
        {
            return false;
        }

        // Date validation
        if (DateTime.Now >= deactivationDate)
            return false;

        return true;
    }

    public bool IsDeactivated()
    {
        if (DateTime.Now >= _periodDateTime.GetFinalDate())
            return true;
        else
            return false;
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
