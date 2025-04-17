
using Domain.Models;

namespace Domain.Visitor;

    public interface ICollaboratorVisitor
    {
        long Id { get; }
        long UserID { get; }
        PeriodDateTime PeriodDateTime { get; }

    }

