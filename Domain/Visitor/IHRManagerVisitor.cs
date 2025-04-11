using Domain.Interfaces;


using Domain.Models;

namespace Domain.Visitor{
    
    public interface IHRManagerVisitor
    {
        long Id { get; }
        long UserId { get; }
        PeriodDateTime PeriodDateTime { get; }
    }
}