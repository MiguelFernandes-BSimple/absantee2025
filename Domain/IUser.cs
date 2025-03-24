public interface IUser
{
    public bool IsDeactivated();
    public bool DeactivationDateIsBefore(DateTime date);

    public bool DeactivateUser();
}