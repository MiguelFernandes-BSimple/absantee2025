namespace Domain;

public class Colaborator : IColaborator
{
    private DateTime _initDate;
    private DateTime _finalDate;
    private IUser _user;

    public Colaborator(IUser user, DateTime initDate, DateTime? finalDate = null)
    {
        if (!finalDate.HasValue)
            finalDate = DateTime.MaxValue;
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
        if (initDate > finalDate)
            return false;

        if (user.DeactivationDateIsBeforeThen(finalDate))
            return false;

        if (user.IsDeactivated())
            return false;
        return true;
    }

    public bool IsInside(DateTime initDate, DateTime finalDate)
    {
        return initDate >= this._initDate && finalDate <= this._finalDate;
    }
}
