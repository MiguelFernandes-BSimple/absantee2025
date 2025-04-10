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

        public void Add(IPeriodDate periodDate, long collabId, long projectId)
        {
            var assoc = _checkAssociationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
            _assocRepository.Add(assoc);
        }
    }
}
