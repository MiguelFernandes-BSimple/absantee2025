using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class AssociationTrainingModuleCollaboratorDataModel : IAssociationTrainingModuleCollaboratorVisitor
{
    public long Id { get; set; }

    public long TrainingModuleId { get; set; }

    public long CollaboratorId { get; set; }

    public AssociationTrainingModuleCollaboratorDataModel()
    {
    }

    public AssociationTrainingModuleCollaboratorDataModel(IAssociationTrainingModuleCollaborator atc)
    {
        Id = atc.Id;
        TrainingModuleId = atc.TrainingModuleId;
        CollaboratorId = atc.CollaboratorId;
    }
}