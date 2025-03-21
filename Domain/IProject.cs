public interface IProject
{
    public bool ContainsDates(DateOnly dataInicio, DateOnly dataFim);
    public bool IsFinished();
}