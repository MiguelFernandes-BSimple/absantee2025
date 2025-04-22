using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

public interface IAssociationProjectCollaboratorVisitor
{
    public long Id { get; }
    public long CollaboratorId { get; }
    public long ProjectId { get; }
    public PeriodDate PeriodDate { get; }
}