
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class AssociationTrainingModuleCollaboratorFactory : IAssociationTrainingModuleCollaboratorFactory
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly ITrainingModuleRepository _trainingModuleRepository;

        public AssociationTrainingModuleCollaboratorFactory(ICollaboratorRepository collaboratorRepository, ITrainingModuleRepository trainingModuleRepository)
        {
            _collaboratorRepository = collaboratorRepository;
            _trainingModuleRepository = trainingModuleRepository;
        }

        public async Task<IAssociationTrainingModuleCollaborator> Create(Guid trainingModuleId, Guid collaboratorId)
        {
            var trainingModule = await _trainingModuleRepository.GetByIdAsync(trainingModuleId);
            var collab = await _collaboratorRepository.GetByIdAsync(collaboratorId);

            if (trainingModule == null)
                throw new ArgumentException("Training Module must exists");

            if (collab == null)
                throw new ArgumentException("Collaborator must exists");

            return new AssociationTrainingModuleCollaborator(trainingModuleId, collaboratorId);
        }

        public AssociationTrainingModuleCollaborator Create(IAssociationTrainingModuleCollaboratorVisitor visitor)
        {
            return new AssociationTrainingModuleCollaborator(visitor.Id, visitor.TrainingModuleId, visitor.CollaboratorId);
        }
    }
}
