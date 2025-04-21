namespace Domain.Visitor
{
    public interface ITrainingSubjectVisitor
    {
        long Id { get; }
        string Title { get; }
        string Description { get; }
    }
}