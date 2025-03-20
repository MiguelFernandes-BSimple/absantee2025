public interface IUser{
    public bool IsDeactivated();
    public bool DeactivationDateIsBeforeThen(DateTime date);
}