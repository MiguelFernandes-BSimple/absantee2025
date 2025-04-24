using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

public interface IProjectVisitor
{
    Guid Id { get; }
    string Title { get; }
    string Acronym { get; }
    PeriodDate PeriodDate { get; }
}
