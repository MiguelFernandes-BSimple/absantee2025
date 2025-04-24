using Domain.Interfaces;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Domain.Models;

public class User : IUser
{
    public long Id { get; set; }
    public string Names { get; set; }
    public string Surnames { get; set; }
    public string Email { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

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

        Names = names;
        Surnames = surnames;
        Email = email;
        PeriodDateTime = new PeriodDateTime(DateTime.UtcNow, (DateTime)deactivationDate);
    }

    public User(long id, string names, string surnames, string email, PeriodDateTime periodDateTime)
    {
        Id = id;
        Names = names;
        Surnames = surnames;
        Email = email;
        PeriodDateTime = periodDateTime;
    }
    public User()
    {

    }

    public long GetId()
    {
        return Id;
    }

    public void SetId(long id)
    {
        Id = id;
    }

    public string GetNames()
    {
        return Names;
    }

    public string GetSurnames()
    {
        return Surnames;
    }

    public string GetEmail()
    {
        return Email;
    }

    public PeriodDateTime GetPeriodDateTime()
    {
        return PeriodDateTime;
    }

    public bool IsDeactivated()
    {
        return PeriodDateTime.IsFinalDateSmallerThan(DateTime.Now);
    }

    public bool DeactivationDateIsBefore(DateTime date)
    {
        return PeriodDateTime.IsFinalDateSmallerThan(date);
    }

    public bool DeactivateUser()
    {
        if (IsDeactivated())
            return false;

        PeriodDateTime.SetFinalDate(DateTime.Now);

        return true;
    }

    public bool HasNames(string names)
    {
        if (string.IsNullOrWhiteSpace(names))
            return false;

        return Names.Contains(names, StringComparison.OrdinalIgnoreCase);
    }

    public bool HasSurnames(string surnames)
    {
        if (string.IsNullOrWhiteSpace(surnames))
            return false;

        return Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase);
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

            if (this.Email.Equals(other.Email))
                return true;
        }

        return false;
    }
}
