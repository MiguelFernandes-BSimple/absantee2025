using Domain.Interfaces;

namespace Domain.Models;

public class AssociationProjectCollaborator : IAssociationProjectCollaborator
{
    private IPeriodDate _periodDate;    
    private ICollaborator _collaborator;
    private IProject _project;

    public AssociationProjectCollaborator(
        IPeriodDate periodDate,
        ICollaborator collaborator,
        IProject project
    )
    {
        if (CheckInputValues(periodDate, collaborator, project))
        {
            _periodDate = periodDate;
            _collaborator = collaborator;
            _project = project;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(
        IPeriodDate periodDate,
        ICollaborator collaborator,
        IProject project
    )
    {
        if (!project.ContainsDates(periodDate))
            return false;

        if (project.IsFinished())
            return false;

        //DateTime associationInitDate = initDate.ToDateTime(TimeOnly.MinValue);
        //DateTime associationFinalDate = finalDate.ToDateTime(TimeOnly.MinValue);
        //if (!collaborator.ContractContainsDates(associationInitDate, associationFinalDate))
        //    return false;

        return true;
    }

    public IPeriodDate GetPeriodDate()
    {
        return _periodDate;
    }

    public IProject GetProject()
    {
        return _project;
    }

    public ICollaborator GetCollaborator()
    {
        return this._collaborator;
    }

    public bool HasCollaborator(ICollaborator collaborator)
    {
        return this._collaborator.Equals(collaborator);
    }

    public bool HasProject(IProject project)
    {
        return this._project.Equals(project);
    }

    public bool AssociationIntersectPeriod(IPeriodDate periodDate)
    {
        return _periodDate.Intersects(periodDate);
    }
}
