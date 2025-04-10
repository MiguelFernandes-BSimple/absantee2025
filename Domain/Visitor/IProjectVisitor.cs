using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor;

public interface IProjectVisitor
{
    long Id { get; }
    string Title { get; }
    string Acronym { get; }
    PeriodDate PeriodDate { get; }
}
