using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Visitor
{
    public interface ITrainingModuleCollaboratorsVisitor
    {
        long Id { get; }
        long TrainingModuleId { get; }
        long CollaboratorId { get; }
    }
}
