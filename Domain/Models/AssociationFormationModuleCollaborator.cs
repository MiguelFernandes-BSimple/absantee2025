using Domain.Interfaces;

namespace Domain.Models;

public class AssociationFormationModuleCollaborator : IAssociationFormationModuleCollaborator
{
    public long _id { get; set; }
    public long _collaboratorId { get; set; }
    public long _formationModuleId { get; set; }
    public PeriodDate _periodDate { get; set; }

    public AssociationFormationModuleCollaborator(long collaboratorId, long formationModuleId, PeriodDate periodDate)
    {
        _collaboratorId = collaboratorId;
        _formationModuleId = formationModuleId;
        _periodDate = periodDate;
    }

    public long GetId()
    {
        return _id;
    }

    public long GetCollaboratorId()
    {
        return _collaboratorId;
    }

    public long GetFormationModuleId()
    {
        return _formationModuleId;
    }
}