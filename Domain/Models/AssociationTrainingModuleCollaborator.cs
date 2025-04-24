using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Models
{
    public class AssociationTrainingModuleCollaborator : IAssociationTrainingModuleCollaborator
    {
        public Guid Id { get; }
        public Guid TrainingModuleId { get; }
        public Guid CollaboratorId { get; }

        public AssociationTrainingModuleCollaborator(Guid trainingModuleId, Guid collaboratorId)
        {
            Id = Guid.NewGuid();
            TrainingModuleId = trainingModuleId;
            CollaboratorId = collaboratorId;
        }

        public AssociationTrainingModuleCollaborator(Guid id, Guid trainingModuleId, Guid collaboratorId)
        {
            Id = id;
            TrainingModuleId = trainingModuleId;
            CollaboratorId = collaboratorId;
        }
    }
}
