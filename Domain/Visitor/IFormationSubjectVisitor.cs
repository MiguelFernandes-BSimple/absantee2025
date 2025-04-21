using Domain.Models;

namespace Domain.Visitor;

public interface IFormationSubjectVisitor
{
    long Id { get; }
    string Title { get; }
    string Description { get; }

}