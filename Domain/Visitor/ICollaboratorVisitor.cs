
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

public interface ICollaboratorVisitor
{
    Guid Id { get; }
    Guid UserId { get; }
    PeriodDateTime PeriodDateTime { get; }

}

