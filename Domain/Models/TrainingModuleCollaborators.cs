using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Models
{
    public class TrainingModuleCollaborators : ITrainingModuleCollaborators
    {
        public long TrainingModuleId { get; }
        public long CollaboratorId { get; }
    }
}
