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

        public AssociationProjectCollaboratorService(IAssociationProjectCollaboratorRepository assocRepository, ICollaboratorRepository collaboratorRepository, IProjectRepository projectRepository)
        {
            _assocRepository = assocRepository;
            _collaboratorRepository = collaboratorRepository;
            _projectRepository = projectRepository;
        }

        public bool Add(IPeriodDate periodDate, long collabId, long projectId)
        {
            var project = _projectRepository.GetById((int)projectId);
            var collab = _collaboratorRepository.GetById((int)collabId);
            if (project == null || collab == null)
            {
                throw new ArgumentException("Invalid Arguments");
            }

            var assoc = new AssociationProjectCollaborator(periodDate, collab, project);
            return _assocRepository.Add(assoc);
        }
    }
}
