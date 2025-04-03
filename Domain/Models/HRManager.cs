using Domain.Interfaces;

namespace Domain.Models;

public class HRManager : IHRManager
{
    private IUser _user;
    private IPeriodDateTime _periodDateTime;

    public HRManager(IUser user, IPeriodDateTime periodDateTime)
    {
        if (periodDateTime.IsFinalDateUndefined())
            periodDateTime.SetFinalDate(DateTime.MaxValue);

        else if (CheckInputFields(user, periodDateTime))
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