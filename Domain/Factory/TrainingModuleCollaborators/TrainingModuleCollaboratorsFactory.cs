
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepository;
using Domain.Models;

namespace Domain.Factory
{
    public class TrainingModuleCollaboratorsFactory
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly ITrainingModuleRepository _trainingModuleRepository;

        public TrainingModuleCollaboratorsFactory(ICollaboratorRepository collaboratorRepository, ITrainingModuleRepository trainingModuleRepository)
        {
            _collaboratorRepository = collaboratorRepository;
            _trainingModuleRepository = trainingModuleRepository;
        }

        public async Task<TrainingModuleCollaborators> Create(long trainingModuleId, long collaboratorId)
        {
            var trainingModule = await _trainingModuleRepository.GetByIdAsync(trainingModuleId);
            var collab = await _collaboratorRepository.GetByIdAsync(collaboratorId);

            if (trainingModule == null)
                throw new ArgumentException("Training Module must exists");

            if (collab == null)
                throw new ArgumentException("Collaborator must exists");

            return new TrainingModuleCollaborators(trainingModuleId, collaboratorId);
        }

    }
}
