using Domain.Interfaces;

namespace Domain.Models;


public class AssociationCollaboratorTrainingModule : IAssociationCollaboratorTrainingModule
{
    public long _id { get; set; }
    public long _collaboratorId { get; set; }
    public long _trainingModuleId { get; set; }
    public PeriodDate _periodDate { get; set; }

    public AssociationCollaboratorTrainingModule(long collaboratorId, long trainingModuleId, PeriodDate periodDate)
    {
        _collaboratorId = collaboratorId;
        _trainingModuleId = trainingModuleId;
        _periodDate = periodDate;
    }

    public bool AssociationIntersectPeriod(PeriodDate periodDate)
    {
        return _periodDate.Intersects(periodDate);
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


    public long GetTrainingModuleId()
    {
        return _trainingModuleId;
    }
}