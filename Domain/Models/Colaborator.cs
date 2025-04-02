using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    private IUser _user;
    private IPeriodDateTime _periodDateTime;

    public Collaborator(IUser user, IPeriodDateTime periodDateTime)
    {
        if (periodDateTime.IsFinalDateUndefined())
            periodDateTime.SetFinalDate(DateTime.MaxValue);

        if (CheckInputFields(user, periodDateTime))
        {
            this._periodDateTime = periodDateTime;
            this._user = user;
        }
        else
            throw new ArgumentException("Invalid Arguments");
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

    public string GetEmail()
    {
        return _user.GetEmail();
    }
}
