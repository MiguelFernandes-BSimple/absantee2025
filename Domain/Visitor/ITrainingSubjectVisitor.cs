namespace Domain.Visitor;

public interface ITrainingSubjectVisitor
{
    Guid Id { get; set; }
    string Subject { get; set; }
    string Description { get; set; }
}
