using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Models
{
    public class AssociationCollabTrainingModule : IAssociationCollabTrainingModule
    {
        public long _id { get; set; }
        public long _collaboratorId { get; set; }
        public long _trainingModuleId { get; set; }

        public AssociationCollabTrainingModule(long id, long collaboratorId, long trainingModuleId)
        {
            _id = id;
            _collaboratorId = collaboratorId;
            _trainingModuleId = trainingModuleId;
        }

        public AssociationCollabTrainingModule(long collaboratorId, long trainingModuleId)
        {
            _collaboratorId = collaboratorId;
            _trainingModuleId = trainingModuleId;
        }

        public long GetId()
        {
            return _id;
        }

        public long GetCollaboratorId()
        {
            return _collaboratorId;
        }

        public long GetTrainingModuleId()
        {
            return _trainingModuleId;
        }
    }
}