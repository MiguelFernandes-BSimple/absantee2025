using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class AssociationTrainingModuleCollaboratorDataModel : IAssociationTrainingModuleCollaboratorVisitor
{
    public long Id {get; set;}
    public long TrainingModuleId {get; set;}
    public long CollaboratorId {get; set;}

    public AssociationTrainingModuleCollaboratorDataModel(AssociationTrainingModuleCollaborator amc) {
        Id = amc.GetId();
        TrainingModuleId = amc.GetTrainingModuleId();
        CollaboratorId = amc.GetTrainingModuleId();
    }        
}
