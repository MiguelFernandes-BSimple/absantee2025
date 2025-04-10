using Domain.Interfaces;

namespace Domain.Models;

public class AssociationProjectCollaborator : IAssociationProjectCollaborator
{
    private long _id;
    private long _collaboratorId;
    public long _projectId;
    private IPeriodDate _periodDate;

    public AssociationProjectCollaborator(long collaboratorId, long projectId, IPeriodDate periodDate)
    {
        _collaboratorId = collaboratorId;
        _projectId = projectId;
        _periodDate = periodDate;
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

            if (_collaboratorId == other._collaboratorId && _projectId == other._projectId
                && _periodDate.Intersects(other._periodDate))
                return true;
        }

        return false;
    }
}
