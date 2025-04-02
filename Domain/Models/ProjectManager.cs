using Domain.Interfaces;

namespace Domain.Models;

public class ProjectManager : IProjectManager
{
    private IUser _user;
    private IPeriodDateTime _periodDateTime;

    public ProjectManager(IUser user, IPeriodDateTime periodDateTime)
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
}