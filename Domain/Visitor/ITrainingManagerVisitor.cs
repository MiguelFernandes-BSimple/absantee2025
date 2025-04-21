
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

    public interface ITrainingManagerVisitor
    {
        long Id { get; }
        long UserID { get; }
        PeriodDateTime PeriodDateTime { get; }

    }

