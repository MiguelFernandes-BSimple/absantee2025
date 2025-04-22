
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

    public interface ICollaboratorVisitor
    {
        long Id { get; }
        long UserId { get; }
        PeriodDateTime PeriodDateTime { get; }

    }

