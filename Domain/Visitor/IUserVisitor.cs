using Domain.Models;

namespace Domain.Visitor;

public interface IUserVisitor
{
    long Id { get; }
    string Names { get; }
    string Surnames { get; }
    string Email { get; }
    PeriodDateTime PeriodDateTime { get; }


}
