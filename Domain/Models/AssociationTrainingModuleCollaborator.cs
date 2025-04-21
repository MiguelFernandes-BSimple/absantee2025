using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Models
{
    public class AssociationTrainingModuleCollaborator : IAssociationTrainingModuleCollaborator
    {
        public long _id { get; set; }
        public long _collaboratorId { get; set; }
        public long _trainingModuleId { get; set; }
        public PeriodDateTime _periodDateTime { get; set; }

        public AssociationTrainingModuleCollaborator(long collaboratorId, long TrainingModuleId, PeriodDateTime periodDateTime)
        {
            _collaboratorId = collaboratorId;
            _trainingModuleId = TrainingModuleId;
            _periodDateTime = periodDateTime;
        }
        public void SetId(long id)
        {
            _id = id;
        }

    }
}