using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.Repositories;

namespace Domain.Factory;

public class AssociationProjectCollaboratorFactory : IAssociationProjectCollaboratorFactory
{
    private ICollaboratorRepository _collaboratorRepository;
    private IProjectRepository _projectRepository;
    private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;

    public AssociationProjectCollaboratorFactory(ICollaboratorRepository collaboratorRepository, IProjectRepository projectRepository, IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository)
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

        if (!project.ContainsDates(periodDate))
            throw new ArgumentException("Invalid arguments");

        if (project.IsFinished())
            throw new ArgumentException("Invalid arguments");

        PeriodDateTime periodDateTime = new PeriodDateTime(periodDate);

        if (!collaborator.ContractContainsDates(periodDateTime))
            throw new ArgumentException("Invalid arguments");

        return new AssociationProjectCollaborator(collaboratorId, projectId, periodDate);
    }

    public AssociationProjectCollaborator Create(IAssociationProjectCollaboratorVisitor associationProjectCollaboratorVisitor)
    {
        return new AssociationProjectCollaborator(
                    associationProjectCollaboratorVisitor.CollaboratorId,
                    associationProjectCollaboratorVisitor.ProjectId,
                    associationProjectCollaboratorVisitor.Period);
    }
}