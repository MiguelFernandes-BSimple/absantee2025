using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAssociationCollabTrainingModule
    {
        public long GetId();
        public long GetCollaboratorId();
        public long GetTrainingModuleId();

    }
}