using Domain.Models;

namespace Domain.Interfaces;

public class IAssociationTrainingModuleCollaborator
{
    public long _id { get; set; }
    public long _collaboratorId { get; set; }
    public long _trainingModuleId { get; set; }
    public PeriodDateTime _periodDateTime { get; set; }
}
