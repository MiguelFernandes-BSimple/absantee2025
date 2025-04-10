using Domain.Interfaces;

namespace Domain.Models;

public class AssociationProjectCollaborator : IAssociationProjectCollaborator
{
    private long _id;
    private long _collaboratorId;
    public long _projectId;
    private IPeriodDate _periodDate;
    private ICollaborator? _collaborator;
    private IProject? _project;

    public AssociationProjectCollaborator(IPeriodDate periodDate, ICollaborator collaborator, IProject project)
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

    public AssociationProjectCollaborator(long collaboratorId, long projectId, IPeriodDate periodDate, ICollaborator collaborator, IProject project)
        : this(periodDate, collaborator, project)
    {
        _collaboratorId = collaboratorId;
        _projectId = projectId;
    }

    public AssociationProjectCollaborator(long Id, long collaboratorId, long projectId, IPeriodDate periodDate)
    {
        _id = Id;
        _collaboratorId = collaboratorId;
        _projectId = projectId;
        _periodDate = periodDate;
    }

    private bool CheckInputValues(IPeriodDate periodDate, ICollaborator collaborator, IProject project)
    {
        if (!project.ContainsDates(periodDate))
            return false;

        if (project.IsFinished())
            return false;

        PeriodDateTime periodDateTime = new PeriodDateTime(periodDate);

        if (!collaborator.ContractContainsDates(periodDateTime))
            return false;

        return true;
    }

    public void SetId(long id)
    {
        _id = id;
    }

    public long GetId()
    {
        return _id;
    }

    public long GetCollaboratorId()
    {
        return _collaboratorId;
    }

    public long GetProjectId()
    {
        return _projectId;
    }

    public IPeriodDate GetPeriodDate()
    {
        return _periodDate;
    }

    public ICollaborator GetCollaborator()
    {
        return _collaborator;
    }

    public IProject GetProject()
    {
        return _project;
    }

    public bool HasCollaborator(ICollaborator collaborator)
    {
        return this._collaborator.Equals(collaborator);
    }

    public bool HasProject(IProject project)
    {
        return this._project.Equals(project);
    }
    public bool HasProjectIds(long projectIds)
    {
        return this._project.GetId() == projectIds;
    }

    public bool AssociationIntersectPeriod(IPeriodDate periodDate)
    {
        return _periodDate.Intersects(periodDate);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj.GetType() == typeof(AssociationProjectCollaborator))
        {
            AssociationProjectCollaborator other = (AssociationProjectCollaborator)obj;

            if (HasCollaborator(other._collaborator) && HasProject(other._project)
                && _periodDate.Intersects(other._periodDate))
                return true;
        }

        return false;
    }
}
