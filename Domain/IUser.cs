public interface IUser
{
    public bool IsDeactivated();
    public bool DeactivationDateIsBefore(DateTime date);
    public bool HasNames(string names);
    public bool HasSurnames(string surnames);

}