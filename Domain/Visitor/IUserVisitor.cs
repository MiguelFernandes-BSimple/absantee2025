using Domain.Models;

namespace Domain.Visitor;

public interface IUserVisitor
{
    Guid Id { get; }
    string Names { get; }
    string Surnames { get; }
    string Email { get; }
    PeriodDateTime PeriodDateTime { get; }
}
