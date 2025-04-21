using Domain.Models;

namespace Domain.Visitor;


public interface ISubjectVisitor
{
    long Id { get; }

    string Titulo { get; }

    string Descricao { get; }

}