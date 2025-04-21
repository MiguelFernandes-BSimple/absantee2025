using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

public interface IAssociationTrainingModuleCollaboratorVisitor
{
    public long Id { get; }
    public long CollaboratorId { get; }
    public long TrainingModuleId { get; }
    public PeriodDateTime PeriodDateTime { get; }
}