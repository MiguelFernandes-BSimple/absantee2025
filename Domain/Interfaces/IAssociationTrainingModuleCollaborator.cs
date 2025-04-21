using Domain.Models;

namespace Domain.Interfaces;

public interface IAssociationTrainingModuleCollaborator
{
    public long Id { get; set; }
    public long CollaboratorId { get; set; }
    public long TrainingModuleId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }
}
