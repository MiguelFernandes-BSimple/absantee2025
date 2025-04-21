using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class AssociationTrainingModuleCollaboratorDataModel : IAssociationTrainingModuleCollaboratorVisitor
{
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }
    public long TrainingModuleId { get; set; }

    public AssociationTrainingModuleCollaboratorDataModel()
    {
    }

    public AssociationTrainingModuleCollaboratorDataModel(IAssociationTrainingModuleCollaborator atc)
    {
        Id = atc._id;
        CollaboratorId = atc._collaboratorId;
        PeriodDateTime = atc._periodDateTime;
        TrainingModuleId = atc._trainingModuleId;
    }
}
