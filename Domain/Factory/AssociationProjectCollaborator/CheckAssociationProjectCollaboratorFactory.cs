using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Repositories;

namespace Domain.Factory;

public class CheckAssociationProjectCollaboratorFactory : ICheckAssociationProjectCollaboratorFactory
{
    private ICollaboratorRepository _collaboratorRepository;
    private IProjectRepository _projectRepository;
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;

    public CheckAssociationProjectCollaboratorFactory(ICollaboratorRepository collaboratorRepository, IProjectRepository projectRepository, IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _projectRepository = projectRepository;
        _associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
    }

    public AssociationProjectCollaborator Create(IPeriodDate periodDate, long collaboratorId, long projectId)
    {
        ICollaborator? collaborator = _collaboratorRepository.GetById((int)collaboratorId);
        IProject? project = _projectRepository.GetById((int)projectId);

        if (collaborator == null || project == null)
            throw new ArgumentException("Invalid arguments");

        return new AssociationProjectCollaborator(periodDate, collaborator, project);
    }
}