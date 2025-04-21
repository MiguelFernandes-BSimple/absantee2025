using Domain.Factory;
using Domain.Models;
using Domain.IRepository;

namespace Application.Services{
    public class AssociationCollaboratorTrainingModuleService
    {
        private IAssociationCollaboratorTrainingModuleRepository _assocRepository;
        private IAssociationCollaboratorTrainingModuleFactory _associationCollaboratorTrainingModuleFactory;

        public AssociationCollaboratorTrainingModuleService(IAssociationCollaboratorTrainingModuleRepository assocRepository, IAssociationCollaboratorTrainingModuleFactory associationCollaboratorTrainingModuleFactory)
        {
            _assocRepository = assocRepository;
            _associationCollaboratorTrainingModuleFactory = associationCollaboratorTrainingModuleFactory;
        }

        public async Task Add(long collaboratorId, long trainingModuleId, PeriodDate periodDate)
        {
            var assoc = await _associationCollaboratorTrainingModuleFactory.Create(collaboratorId,trainingModuleId,periodDate);
            await _assocRepository.AddAsync(assoc);
        }

    }
}

