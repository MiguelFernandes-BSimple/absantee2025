using Domain.Models;

namespace Domain.Interfaces;

public interface IUser
{
    public Guid Id { get; }
    public string Names { get; }
    public string Surnames { get; }
    public string Email { get; }
    public PeriodDateTime PeriodDateTime { get; }
    public bool IsDeactivated();
    public bool DeactivationDateIsBefore(DateTime date);
    public bool DeactivateUser();
    public bool HasNames(string names);
    public bool HasSurnames(string surnames);
    public bool Equals(Object? obj);
    void UpdateEmail(string email);
    void UpdateName(string name);
    void UpdateSurname(string surname);
    void UpdatePeriod(PeriodDateTime period);
}