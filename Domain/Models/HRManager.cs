using Domain.Interfaces;
namespace Domain.Models;

public class HRManager : IHRManager
{
    private DateTime _initDate;
    private DateTime _finalDate;
    private IUser _user;

    public HRManager(IUser user, DateTime initDate, DateTime? finalDate)
    {
        finalDate ??= DateTime.MaxValue;
        if (checkInputFields(initDate, (DateTime)finalDate, user))
        {
            this._initDate = initDate;
            this._finalDate = (DateTime)finalDate;
            this._user = user;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool checkInputFields(DateTime initDate, DateTime finalDate, IUser user)
    {
        if (initDate >= finalDate)
            return false;

        if (user.DeactivationDateIsBefore(finalDate))
            return false;

        if (user.IsDeactivated())
            return false;

        return true;
    }
}