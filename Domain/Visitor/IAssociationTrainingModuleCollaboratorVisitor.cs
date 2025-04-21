namespace Domain.Visitor;

public interface IAssociationTrainingModuleCollaboratorVisitor {
    long Id {get;}
    long TrainingModuleId {get;}
    long CollaboratorId {get;}
}
