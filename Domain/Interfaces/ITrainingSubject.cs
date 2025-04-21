namespace Domain.Interfaces;

public interface ITrainingSubject
{
    public long Id { get; }
    public string Title { get; }
    public string Description { get; }
}