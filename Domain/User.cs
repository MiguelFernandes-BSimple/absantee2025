using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Domain;
public class User : IUser
{
    private string _names;
    private string _surnames;
    private string _email;

    private DateTime _creationDate;
    private DateTime? _deactivationDate;

    public User(string names, string surnames, string email, DateTime? deactivationDate)
    {
        deactivationDate ??= DateTime.MaxValue;

        if (CheckInputValues(names, surnames, email, (DateTime)deactivationDate))
        {
            _names = names;
            _surnames = surnames;
            _email = email;
            _creationDate = DateTime.Now;
            _deactivationDate = (DateTime)deactivationDate;

        }
        else
        {
            throw new ArgumentException("Invalid Arguments");
        }
    }

    private bool CheckInputValues(string names, string surnames, string email, DateTime deactivationDate)
    {
        Regex nameRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,50}$");

        if (!nameRegex.IsMatch(names) || !nameRegex.IsMatch(surnames))
        {
            return false;
        }

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
        {
            return false;
        }

        return true;
    }

    public bool IsDeactivated()
    {
        if (DateTime.Now >= _deactivationDate)
            return true;
        else
            return false;
    }

    public bool DeactivationDateIsBefore(DateTime date)
    {
        return date > _deactivationDate;
    }

    public bool HasNames(string names)
    {
        // Return false if 'names' is null, empty, or contains only whitespace
        if (string.IsNullOrWhiteSpace(names))
        {
            return false;
        }

        // Split both names and _names into individual words
        var nameWords = names.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var _nameWords = _names.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Check if every word in 'names' exists in '_names' (case insensitive)
        return nameWords.All(name => _nameWords.Any(word => word.Equals(name, StringComparison.OrdinalIgnoreCase)));
    }

    public bool HasSurnames(string surnames)
    {
        // Return false if 'names' is null, empty, or contains only whitespace
        if (string.IsNullOrWhiteSpace(surnames))
        {
            return false;
        }
        // Split both surnames and _surnames into individual words
        var surnameWords = surnames.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var _surnameWords = _surnames.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Check if every word in 'surnames' exists in '_surnames' (case insensitive)
        return surnameWords.All(surname => _surnameWords.Any(word => word.Equals(surname, StringComparison.OrdinalIgnoreCase)));
    }
}