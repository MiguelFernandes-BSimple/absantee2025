public interface IProject
{
    public bool ContainsDates(DateOnly intiDate, DateOnly finalDate);
    public bool IsFinished();
}