using Infrastructure.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class AssociationProjectCollaboratorRepository : IAssociationProjectCollaboratorRepository
{
    private List<IAssociationProjectCollaborator> _associationsProjectCollaborator;

    public AssociationProjectCollaboratorRepository(List<IAssociationProjectCollaborator> associationsProjectCollaborator)
    {
        if(CheckInputValue(associationsProjectCollaborator))
            _associationsProjectCollaborator = associationsProjectCollaborator;
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProject(IProject project)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project));
    }
    public IAssociationProjectCollaborator? FindByProjectAndCollaborator(IProject project, ICollaborator collaborator)
    {
        return _associationsProjectCollaborator.Where(a => a.HasProject(project) && a.HasCollaborator(collaborator)).FirstOrDefault();
    }

    public IEnumerable<IAssociationProjectCollaborator> FindAllByProjectAndBetweenPeriod(IProject project, IPeriodDate periodDate)
    {
        return _associationsProjectCollaborator
                .Where(a => a.HasProject(project)
                    && a.AssociationIntersectPeriod(periodDate));
    }

    public bool Add(IAssociationProjectCollaborator newAssociation)
    {
        if (IsRepeated(newAssociation))
        {
            return false;
        }
        else
        {
            _associationsProjectCollaborator.Add(newAssociation);
            return true;
        }
    }

    private bool CheckInputValue(List<IAssociationProjectCollaborator> associationsProjectCollaborator)
    {
        for (int i = 0; i < associationsProjectCollaborator.Count - 1; i++)
        {
            if (IsRepeated(
                    associationsProjectCollaborator[i],
                    associationsProjectCollaborator.Skip(i + 1).ToList()))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsRepeated(IAssociationProjectCollaborator newAssociation)
    {
        return IsRepeated(newAssociation, _associationsProjectCollaborator);
    }

    private bool IsRepeated(IAssociationProjectCollaborator newAssociation, List<IAssociationProjectCollaborator> associationsProjectCollaborator)
    {
        return associationsProjectCollaborator.Any(a => a.Equals(newAssociation));
    }
}
