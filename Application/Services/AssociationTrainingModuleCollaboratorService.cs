using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;

namespace Application.Services
{
    public class AssociationTrainingSubjectCollaborator
    {
        private IAssociationTrainingModuleCollaboratorRepository _assocTCRepository;
        private ITrainingModuleRepository _trainingModuleRepository;
        private ICollaboratorRepository _collabRepository;
        private IAssociationTrainingModuleCollaboratorFactory _assocTCFactory;

        public AssociationTrainingSubjectCollaborator(IAssociationTrainingModuleCollaboratorRepository assocTCRepository, ITrainingModuleRepository trainingModuleRepository, ICollaboratorRepository collabRepository, IAssociationTrainingModuleCollaboratorFactory assocTCFactory)
        {
            _assocTCRepository = assocTCRepository;
            _trainingModuleRepository = trainingModuleRepository;
            _collabRepository = collabRepository;
            _assocTCFactory = assocTCFactory;
        }

        public async Task Add(long trainingModuleId, long collaboratorId)
        {
            ITrainingModule? tm = await _trainingModuleRepository.GetByIdAsync(trainingModuleId);
            ICollaborator? collab = await _collabRepository.GetByIdAsync(collaboratorId);

            if (tm == null || collab == null)
                throw new ArgumentException("Invalid arguments");

            var assoc = await _assocTCFactory.Create(trainingModuleId, collaboratorId);

            await _assocTCRepository.AddAsync(assoc);
        }
    }
}
