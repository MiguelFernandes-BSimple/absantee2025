using Domain.Interfaces;

namespace Domain.Models;

public class HRManager : IHRManager
{
    private IUser _user;
    private IPeriodDateTime _periodDateTime;

    public HRManager(IUser user, IPeriodDateTime periodDateTime)
    {
        if (CheckInputFields(user, periodDateTime))
        {
            this._periodDateTime = periodDateTime;
            this._user = user;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public HRManager(IUser user, DateTime initDate) :
        this(user, new PeriodDateTime(initDate, DateTime.MaxValue))
    {
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