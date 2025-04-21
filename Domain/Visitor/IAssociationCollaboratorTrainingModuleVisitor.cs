using Domain.Models;


namespace Domain.Visitor;


public interface IAssociationCollaboratorTrainingModuleVisitor
{
    public long Id { get; }
    public long CollaboratorId { get; }
    public long TrainingModuleId  { get; }
    public PeriodDate Period  { get; }


}