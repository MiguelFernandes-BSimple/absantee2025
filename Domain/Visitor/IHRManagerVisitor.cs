using Domain.Interfaces;


using Domain.Models;

namespace Domain.Visitor{
    
    public interface IHRManagerVisitor
    {
        Guid Id { get; }
        Guid UserId { get; }
        PeriodDateTime PeriodDateTime { get; }
    }
}