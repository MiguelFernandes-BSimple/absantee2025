using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;

namespace Application.Services
{
    public class AssociationProjectCollaboratorService
    {
        private IAssociationProjectCollaboratorRepository _assocRepository;
        private IAssociationProjectCollaboratorFactory _associationProjectCollaboratorFactory;

        public AssociationProjectCollaboratorService(IAssociationProjectCollaboratorRepository assocRepository, IAssociationProjectCollaboratorFactory associationProjectCollaboratorFactory)
        {
            _assocRepository = assocRepository;
            _associationProjectCollaboratorFactory = associationProjectCollaboratorFactory;
        }

        public async void Add(IPeriodDate periodDate, long collabId, long projectId)
        {
            var assoc = await _associationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
            if (assoc != null)
                await _assocRepository.AddAsync(assoc);
        }
    }
}
