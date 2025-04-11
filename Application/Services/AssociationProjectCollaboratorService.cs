using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;

namespace Application.Services
{
    public class AssociationProjectCollaboratorService
    {
        private IAssociationProjectCollaboratorRepository _assocRepository;
        private IAssociationProjectCollaboratorFactory _checkAssociationProjectCollaboratorFactory;

        public AssociationProjectCollaboratorService(IAssociationProjectCollaboratorRepository assocRepository, IAssociationProjectCollaboratorFactory checkAssociationProjectCollaboratorFactory)
        {
            _assocRepository = assocRepository;
            _checkAssociationProjectCollaboratorFactory = checkAssociationProjectCollaboratorFactory;
        }

        public async Task<bool> Add(IPeriodDate periodDate, long collabId, long projectId)
        {
            var assoc = await _checkAssociationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
            if (assoc != null)
                return await _assocRepository.AddAsync(assoc);

            return false;
        }
    }
}
