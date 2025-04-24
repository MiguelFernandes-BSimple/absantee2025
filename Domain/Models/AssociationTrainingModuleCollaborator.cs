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
        public Guid Id { get; set; }
        public Guid TrainingModuleId { get; }
        public Guid CollaboratorId { get; }

        public AssociationTrainingModuleCollaborator(Guid trainingModuleId, Guid collaboratorId)
        {
            TrainingModuleId = trainingModuleId;
            CollaboratorId = collaboratorId;
        }
    }
}
