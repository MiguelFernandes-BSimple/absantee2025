using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class AssociationTrainingModuleCollaboratorDataModel : IAssociationTrainingModuleCollaboratorVisitor
    {
        public Guid Id { get; set; }
        public Guid TrainingModuleId { get; set; }
        public Guid CollaboratorId { get; set; }

        public AssociationTrainingModuleCollaboratorDataModel()
        {
        }

        public AssociationTrainingModuleCollaboratorDataModel(IAssociationTrainingModuleCollaborator trainingModuleCollaborators)
        {
            Id = trainingModuleCollaborators.Id;
            TrainingModuleId = trainingModuleCollaborators.TrainingModuleId;
            CollaboratorId = trainingModuleCollaborators.CollaboratorId;
        }
    }
}
