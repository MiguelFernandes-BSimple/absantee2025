using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class AssociationProjectCollaboratorService
    {
        private IAssociationProjectCollaboratorRepository _assocRepository;
        private ICollaboratorRepository _collaboratorRepository;
        private IProjectRepository _projectRepository;
        private ICheckAssociationProjectCollaboratorFactory _checkAssociationProjectCollaboratorFactory;

        public AssociationProjectCollaboratorService(IAssociationProjectCollaboratorRepository assocRepository, ICollaboratorRepository collaboratorRepository, IProjectRepository projectRepository, ICheckAssociationProjectCollaboratorFactory checkAssociationProjectCollaboratorFactory)
        {
            _assocRepository = assocRepository;
            _collaboratorRepository = collaboratorRepository;
            _projectRepository = projectRepository;
            _checkAssociationProjectCollaboratorFactory = checkAssociationProjectCollaboratorFactory;
        }

        public void Add(IPeriodDate periodDate, long collabId, long projectId)
        {
            var assoc = _checkAssociationProjectCollaboratorFactory.Create(periodDate, collabId, projectId);
            _assocRepository.Add(assoc);
        }
    }
}
