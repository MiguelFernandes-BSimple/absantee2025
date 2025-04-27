using Domain.Interfaces;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Domain.Models
{
    public class User : IUser
    {
        public long Id { get; private set; }
        public string Names { get; private set; }
        public string Surnames { get; private set; }
        public string Email { get; private set; }
        public PeriodDateTime PeriodDateTime { get; private set; }

        // Construtor vazio exigido pelo EF Core / serialização
        public User() { }

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

            if (DateTime.UtcNow >= deactivationDate.Value.ToUniversalTime())
                throw new ArgumentException("Deactivation date can't be in the past.");

            Names = names;
            Surnames = surnames;
            Email = email;
            PeriodDateTime = new PeriodDateTime(
                DateTime.UtcNow,
                deactivationDate.Value.ToUniversalTime()
            );
        }

        public User(long id, string names, string surnames, string email, PeriodDateTime periodDateTime)
        {
            Id = id;
            Names = names;
            Surnames = surnames;
            Email = email;
            PeriodDateTime = periodDateTime;
        }

        public void SetId(long id)
        {
            Id = id;
        }

        // 🔁 Métodos GetX()
        public long GetId() => Id;
        public string GetNames() => Names;
        public string GetSurnames() => Surnames;
        public string GetEmail() => Email;
        public PeriodDateTime GetPeriodDateTime() => PeriodDateTime;

        public bool IsDeactivated()
        {
            return PeriodDateTime.IsFinalDateSmallerThan(DateTime.UtcNow);
        }

        public bool DeactivationDateIsBefore(DateTime date)
        {
            return PeriodDateTime.IsFinalDateSmallerThan(date);
        }

        public bool DeactivateUser()
        {
            if (IsDeactivated())
                return false;

            PeriodDateTime.SetFinalDate(DateTime.UtcNow);
            return true;
        }

        public bool HasNames(string names)
        {
            return !string.IsNullOrWhiteSpace(names) &&
                   Names.Contains(names, StringComparison.OrdinalIgnoreCase);
        }

        public bool HasSurnames(string surnames)
        {
            return !string.IsNullOrWhiteSpace(surnames) &&
                   Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not User other)
                return false;

            return Email.Equals(other.Email, StringComparison.OrdinalIgnoreCase);
        }
    }
}
