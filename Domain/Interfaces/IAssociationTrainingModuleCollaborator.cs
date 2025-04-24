using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAssociationTrainingModuleCollaborator
    {
        Guid Id { get; }
        Guid TrainingModuleId { get; }
        Guid CollaboratorId { get; }
    }
}
