using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Visitor
{
    public interface AssociationTrainingModuleCollaboratorVisitor
    {
        Guid Id { get; }
        Guid TrainingModuleId { get; }
        Guid CollaboratorId { get; }
    }
}
