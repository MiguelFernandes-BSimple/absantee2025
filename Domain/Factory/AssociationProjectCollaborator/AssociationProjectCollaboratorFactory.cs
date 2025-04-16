using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class AssociationProjectCollaboratorFactory : IAssociationProjectCollaboratorFactory
{
    private ICollaboratorRepository _collaboratorRepository;
    private IProjectRepository _projectRepository;
    private IAssociationProjectCollaboratorRepository _associationProjectRepository;
    public AssociationProjectCollaboratorFactory(ICollaboratorRepository collaboratorRepository, IProjectRepository projectRepository, IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _projectRepository = projectRepository;
        _associationProjectRepository = associationProjectCollaboratorRepository;
    }

    public async Task<AssociationProjectCollaborator> Create(IPeriodDate periodDate, long collaboratorId, long projectId)
    {
        // Checking if input values are valid
        ICollaborator? collaborator = _collaboratorRepository.GetById(collaboratorId);
        IProject? project = _projectRepository.GetById(projectId);

        if (collaborator == null || project == null)
            throw new ArgumentException("Invalid arguments");

        if (!project.ContainsDates(periodDate))
            throw new ArgumentException("Invalid arguments");

        if (project.IsFinished())
            throw new ArgumentException("Invalid arguments");

        PeriodDateTime periodDateTime = new PeriodDateTime(periodDate);

        if (!collaborator.ContractContainsDates(periodDateTime))
            throw new ArgumentException("Invalid arguments");

        // Checking Association's unicity
        // There can't be two associations with the same collab and project 
        // WHERE the periods intersect
        bool canInsert = await _associationProjectRepository.CanInsert(periodDate, collaboratorId, projectId);

        if (!canInsert)
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