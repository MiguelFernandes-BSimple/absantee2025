using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITrainingModuleCollaborators
    {
        long Id { get; }
        long TrainingModuleId { get; }
        long CollaboratorId { get; }
    }
}
