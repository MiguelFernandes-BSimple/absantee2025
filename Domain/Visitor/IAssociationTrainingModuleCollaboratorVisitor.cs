namespace Domain.Visitor;

public interface IAssociationTrainingModuleCollaboratorVisitor
{
    public long Id { get; }
    public long TrainingModuleId { get; }
    public long CollaboratorId { get; }
}