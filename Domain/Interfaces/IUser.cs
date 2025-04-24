using Domain.Models;

namespace Domain.Interfaces;

public interface IUser
{
    public long Id { get; set; }
    public string Names { get; set; }
    public string Surnames { get; set; }
    public string Email { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public bool IsDeactivated();
    public bool DeactivationDateIsBefore(DateTime date);
    public bool DeactivateUser();
    /**
    * Method to verify if a given string is found inside the names of an User
    * It just needs to contain the string, it doesn't need to be the exact same
    * Returns true  -> if the User parameter contains the string names
    * Returns false -> otherwise; or in case its a invalid string (null, whitespaces or empty)
    */
    public bool HasNames(string names);

    /**
    * Method to verify if a given string is found inside the surnames of an User
    * It just needs to contain the string, it doesn't need to be the exact same
    * Returns true  -> if the User parameter contains the string surnames
    * Returns false -> otherwise; or in case its a invalid string (null, whitespaces or empty)
    */
    public bool HasSurnames(string surnames);

    public bool Equals(Object? obj);

    public long GetId();
    public string GetNames();
    public string GetSurnames();
    public string GetEmail();
    public PeriodDateTime GetPeriodDateTime();
}