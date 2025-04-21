using Domain.Models;

namespace Domain.Interfaces;


public interface IAssociationCollaboratorTrainingModule
{
    public long _id { get; set; }

    public long _collaboratorId { get; set; }

    public long _trainingModuleId { get; set; }

    public PeriodDate _periodDate { get; set; }

    public long GetId();

    public long GetCollaboratorId();

    public long GetTrainingModuleId();

    public bool AssociationIntersectPeriod(PeriodDate periodDate);

}