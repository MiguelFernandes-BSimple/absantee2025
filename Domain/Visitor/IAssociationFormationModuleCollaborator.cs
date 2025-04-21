using Domain.Models;

namespace Domain.Visitor;

public interface IAssociationFormationModuleCollaboratorVisitor
{
    public long Id { get; }
    public long CollaboratorId { get; }
    public long FormationModuleId { get; }
    public PeriodDate Period { get; }
}