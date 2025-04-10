using Domain.Interfaces;

namespace Domain.Visitor;

public interface IAssociationProjectCollaboratorVisitor
{
    public long Id { get; }
    public long CollaboratorId { get; }
    public long ProjectId { get; }
    public IPeriodDate Period { get; }
}