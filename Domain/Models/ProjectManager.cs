using Domain.Interfaces;
namespace Domain.Models;

public class ProjectManager : IProjectManager
{
    private DateTime _initDate;
    private DateTime _finalDate;
    private IUser _user;

    public ProjectManager(IUser _user, DateTime _initDate, DateTime? _finalDate)
    {
        _finalDate ??= DateTime.MaxValue;

        if (checkInputFields(_initDate, (DateTime)_finalDate, _user))
        {

            this._initDate = _initDate;
            this._finalDate = (DateTime)_finalDate;
            this._user = _user;

        }
        else
        {
            throw new ArgumentException("Invalid arguments.");
        }
    }

    private bool checkInputFields(DateTime initDate, DateTime finalDate, IUser user)
    {
        if (initDate > finalDate)
            return false;

        if (user.DeactivationDateIsBefore(finalDate))
            return false;

        if (user.IsDeactivated())
            return false;

        return true;
    }
}