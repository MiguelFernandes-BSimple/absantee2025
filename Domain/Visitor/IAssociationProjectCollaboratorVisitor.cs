using Domain.Models;

namespace Domain.Visitor;

public interface IAssociationProjectCollaboratorVisitor
{
    public Guid Id { get; }
    public Guid CollaboratorId { get; }
    public Guid ProjectId { get; }
    public PeriodDate PeriodDate { get; }
}