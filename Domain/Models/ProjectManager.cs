using Domain.Interfaces;

namespace Domain.Models;

public class ProjectManager : IProjectManager
{
    private long _id;
    private IUser _user;
    private IPeriodDateTime _periodDateTime;

    public ProjectManager(IUser user, IPeriodDateTime periodDateTime)
    {
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

    public ProjectManager(IUser user, DateTime initDate) :
        this(user, new PeriodDateTime(initDate, DateTime.MaxValue))
    {
    }

    public long GetId()
    {
        return _id;
    }

    public void SetId(long id)
    {
        _id = id;
    }
    public IUser GetUser()
    {
        return _user;
    }

    public IPeriodDateTime GetPeriodDateTime()
    {
        return _periodDateTime;
    }
}