using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("AssociationTrainingModuleCollaborator")]

public class AssociationTrainingModuleCollaboratorDataModel : IAssociationTrainingModuleCollaboratorVisitor
{
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public long TrainingModuleId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public AssociationTrainingModuleCollaboratorDataModel() { }

    public AssociationTrainingModuleCollaboratorDataModel(IAssociationTrainingModuleCollaborator atmc)
    {
        Id = atmc.Id;
        CollaboratorId = atmc.CollaboratorId;
        TrainingModuleId = atmc.TrainingModuleId;
        PeriodDateTime = atmc.PeriodDateTime;
    }
}
