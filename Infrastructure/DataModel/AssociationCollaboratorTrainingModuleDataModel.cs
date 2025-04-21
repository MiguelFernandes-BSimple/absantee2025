using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class AssociationCollaboratorTrainingModuleDataModel : IAssociationCollaboratorTrainingModuleVisitor
    {
        public long Id { get; set; }

        public long CollaboratorId { get; set; }

        public long TrainingModuleId { get; set; }

        public PeriodDate Period { get; set; }

        public AssociationCollaboratorTrainingModuleDataModel()
        {}

        public AssociationCollaboratorTrainingModuleDataModel(IAssociationCollaboratorTrainingModule associationCollaboratorTrainingModule)
        {
            Id = associationCollaboratorTrainingModule._id;
            CollaboratorId = associationCollaboratorTrainingModule._collaboratorId;
            TrainingModuleId = associationCollaboratorTrainingModule._trainingModuleId;
            Period = associationCollaboratorTrainingModule._periodDate;
        }
    }
}