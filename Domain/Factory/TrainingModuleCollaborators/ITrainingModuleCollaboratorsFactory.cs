using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Factory
{
    public interface ITrainingModuleCollaboratorsFactory
    {
        Task<TrainingModuleCollaborators> Create(long trainingModuleId, long collaboratorId);
    }
}
